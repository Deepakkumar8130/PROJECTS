
/*----- PROC 1 -----*/
/*----- AUTHENTICATE USER -----*/
CREATE PROCEDURE USP_GET_AUTHENTICATE
@email VARCHAR(100),
@pass VARCHAR(100)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM User_Master(NOLOCK) WHERE Email = @email AND Status = 1)
		BEGIN
			IF EXISTS (SELECT 1 FROM User_Master(NOLOCK) WHERE Email = @email AND Password = DBO.HashPassword(@pass))
				BEGIN
					SELECT 1 AS RESULT;
				END
			ELSE
				BEGIN
					SELECT 2 AS RESULT;
				END
		END
	ELSE
		BEGIN
			SELECT 3 AS RESULT;
		END
END

/*----- FOR LOGIN CHECKING-----*/
EXEC USP_GET_AUTHENTICATE 'rahul123@gmail.com','rh@123'
-----
EXEC USP_GET_AUTHENTICATE 'rahul123@gmail.com','smt@123'
-----
EXEC USP_GET_AUTHENTICATE 'rahul1243@gmail.com','smt@123'


GO;

/*----- PROC 2 -----*/
/*----- GET PROGRAMS THROUGH USERID -----*/
CREATE PROCEDURE USP_GET_PROGRAMS
@userId INT
AS
BEGIN
	SELECT prog.Id, prog.P_title Title, prog.Path Path, prog.Descr Description
	FROM Tbl_Rights(NOLOCK) tbl
	INNER JOIN Program_Master(NOLOCK) prog
	ON prog.Id = tbl.Programe_id
	where tbl.UserId = @userId
	AND tbl.Status = 1 
	AND prog.Status = 1
	OR tbL.RoleId IN (SELECT RoleId FROM Role_Employee_Mapping(NOLOCK) WHERE EmpId = @userId)
	ORDER BY prog.Display_Sequence;
END

/*----- FOR CHECKING-----*/
EXEC USP_GET_PROGRAMS 1
---------
EXEC USP_GET_PROGRAMS 6


/*----- PROC 3 -----*/
/*----- GET USER THROUGH EMAIL -----*/
CREATE PROCEDURE USP_GET_USER_BY_EMAIL
@email VARCHAR(100)
AS
BEGIN
	SELECT um.Id, um.Nm, um.Email, um.Mobile, um.Manager_Id, um.Status, rm.Role
		FROM User_Master(NOLOCK) um
		INNER JOIN Role_Employee_Mapping(NOLOCK) rem
		ON um.Id = rem.EmpId
		INNER JOIN Role_Master(NOLOCK) rm
		ON rem.RoleId = rm.Id
		WHERE um.Email = @email;
END

/*----- FOR CHECKING-----*/
EXEC USP_GET_USER_BY_EMAIL 'rahul123@gmail.com'
-----------
EXEC USP_GET_USER_BY_EMAIL 'rahul1243@gmail.com'



/*----- PROC 4 -----*/
/*----- RAISE CLAIM REQUEST -----*/
CREATE PROCEDURE USP_RAISE_CLAIM_REQUEST
@UserId INT,
@Claim_Title VARCHAR(100),
@Claim_Reason VARCHAR(100),
@Amount DECIMAL,
@Evidence VARCHAR(500) = NULL,
@ExpenseDt VARCHAR(100),
@Claim_Description VARCHAR(500)
AS
BEGIN
	DECLARE @Current_Status VARCHAR(100)
	DECLARE @ClaimId INT
	SELECT @Current_Status =NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = 'Initiated'
	
		
		/*CHECK CLAIM TABLE USER ALREADY RAISED CLAIM OR NOT*/
		IF EXISTS (SELECT 1 FROM Claim_Master(NOLOCK) WHERE UserId = @UserId AND CurrentStatus LIKE '%Pending%')
			BEGIN
			  THROW 50000, 'Claim already in pending', 1
			--RAISERROR('Claim Already In Pending', 1, 1);
			  RETURN;
			END
		BEGIN TRY
			BEGIN TRAN Trn_Claim;
		/*INSERT INTO CLAIM RECORD TABLE*/
			INSERT INTO Claim_Master (
										UserId ,
										Claim_Title ,
										Claim_Reason,
										Amount,
										ClaimDt ,
										Evidence,
										ExpenseDt,
										Claim_Description,
										CurrentStatus,
										Status
										)
									VALUES
									( @UserId,
									  @Claim_Title,
									  @Claim_Reason,
									  @Amount,
									  GETDATE(),
									  @Evidence,
									  @ExpenseDt,
									  @Claim_Description,
									  @Current_Status,
									  1
									)
		-----------
		/*INSERT INTO CLAIM ACTION TABLE*/
		SET @ClaimId = SCOPE_IDENTITY();

		INSERT INTO Employee_Claim_Action(
											ClaimId,
											Action,
											ActionBy,
											ActionDt,
											Remarks,
											Status
											)
										VALUES
										( @ClaimId,
										  'Initiated',
										  @UserId,
										  GETDATE(),
										  @Claim_Description,
										  1
										)
		COMMIT TRAN Trn_Claim;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN Trn_Claim;
		END CATCH
END


/*----- FOR CHECKING-----*/
declare @dt DATETIME=GETDATE()
/*UserId INT, Claim_Title VARCHAR(100), Claim_Reason VARCHAR(100), Amount DECIMAL, Evidence VARCHAR(500) = NULL, ExpenseDt DATETIME, Claim_Description VARCHAR(500)*/
EXEC USP_RAISE_CLAIM_REQUEST 2,'Petrol Expense for OCT month','Travel', 5000,'EVIDENCE.JPG',@dt,'required claim amount'
-----------
declare @dt DATETIME=GETDATE()
EXEC USP_RAISE_CLAIM_REQUEST 3,'Petrol Expense for NOV month','Travel', 5000,'EVIDENCE.JPG',@dt,'required claim amount'


/*----- PROC 5 -----*/
/*----- GET CLAIM PENDING REQUEST -----*/
CREATE PROCEDURE USP_GET_PENDING_REQUEST
@role VARCHAR(20),
@userId INT
AS
BEGIN
	SELECT  cm.Id ClaimId,
			um.Nm EmployeeName,
			cm.Claim_Title ClaimTitle,
			cm.Claim_Reason ClaimReason,
			cm.Amount ClaimAmount,
			cm.ClaimDt ClaimDt,
			cm.Evidence ClaimEvidence,
			cm.ExpenseDt ClaimExpenseDt,
			cm.Claim_Description ClaimDescription,
			cm.CurrentStatus CurrentStatus
			FROM Claim_Master(NOLOCK) cm
			INNER JOIN User_Master(NOLOCK) um
			ON cm.UserId = um.Id
			WHERE cm.CurrentStatus = (SELECT rm.Action FROM Employee_Claim_Role_Master(NOLOCK) rm
												WHERE rm.Role = @role)
			AND um.Manager_Id = CASE WHEN @role = 'Manager'
										THEN @userId
										ELSE um.Manager_Id
								END;
END


/*----- FOR CHECKING-----*/
EXEC USP_GET_PENDING_REQUEST 'Manager',1


/*----- PROC 6 -----*/
/*----- UPDATE CLAIM STATUS (APPROVE OR REJECT) -----*/
CREATE PROCEDURE USP_UPDATE_CLAIM
@claimId INT,
@action TINYINT, --1 FOR APPROVE & 0 FOR REJECT--
@userId INT,
@role VARCHAR(20),
@remark VARCHAR(200)
AS
BEGIN
	DECLARE @current_status VARCHAR(100);
	DECLARE @next_action VARCHAR(100);

	SELECT @current_status = CurrentStatus FROM Claim_Master(NOLOCK)
							 WHERE Id = @claimId;
	
	SELECT @next_action = NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = @current_status AND Status = @action;
	BEGIN TRAN Trn_Update_Claim;
		BEGIN TRY
		UPDATE Claim_Master SET CurrentStatus = @next_action
							WHERE Id = @claimId
		INSERT INTO Employee_Claim_Action(
											ClaimId,
											Action,
											ActionBy,
											ActionDt,
											Remarks,
											Status
											)
										VALUES(
											@claimId,
											@next_action,
											@userId,
											GETDATE(),
											@remark,
											1
										);
		IF @role = 'Account'
			BEGIN
				EXEC USP_CLAIM_TRANSACTION_ENTRY @userId, @claimId
			END
		COMMIT TRAN Trn_Update_Claim;
		END TRY
		BEGIN CATCH
		ROLLBACK TRAN Trn_Update_Claim;
		END CATCH
END


/*----- FOR CHECKING-----*/
/*EXEC USP_UPDATE_CLAIM @claimId INT, @action TINYINT, @userId INT, @role VARCHAR(20), @remark VARCHAR(200)*/
EXEC USP_UPDATE_CLAIM 1,1,1,'Manager','APPROVE BY MANAGER'
---------
EXEC USP_UPDATE_CLAIM 1,1,4,'HR','APPROVE BY HR'



/*----- PROC 7 -----*/
/*----- GET CLAIM ACTION HISTORY -----*/
CREATE PROCEDURE USP_GET_CLAIM_ACTION_HISTORY
@claimId INT
AS
BEGIN
	SELECT
		CASE
			WHEN eca.Action = 'Initiated' THEN 'Raise A Claim Request'
			WHEN eca.Action = 'Pending At HR' THEN 'Approved By Manager'
			WHEN eca.Action = 'Pending At Account' THEN 'Approved By HR'
			WHEN eca.Action = 'Completed' THEN 'Claim Raised Successfully !!!'
			ELSE eca.Action
			END 'Action',
		um.Nm 'Name',
		eca.Remarks 'Remark',
		eca.ActionDt 'Action Date'
		FROM Employee_Claim_Action(NOLOCK) eca
		INNER JOIN User_Master(NOLOCK) um
		ON eca.ActionBy = um.Id;
END


/*----- FOR CHECKING-----*/
EXEC USP_GET_CLAIM_ACTION_HISTORY 1



/*----- PROC 8 -----*/
/*----- CLAIM TRANSACTION ENTRY -----*/
CREATE PROCEDURE USP_CLAIM_TRANSACTION_ENTRY
@UserId INT,--THIS USER ID IS WHO APPROVED THIS CLAIM LIKE MANAGER, HR, ACCOUNT SO ON.--
@ClaimId INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN claim_tran
			DECLARE @amount DECIMAL;
			SELECT @amount = Amount FROM Claim_Master(NOLOCK) WHERE Id = @ClaimId;
			INSERT INTO Employee_Claim_Transaction(
									Transaction_No,
									Employee_Id,
									Amount,
									TransactionDt,
									CliamId,
									Status
									) VALUES(
									4548,
									@UserId,
									@amount,
									GETDATE(),
									@ClaimId,
									1
									);
		COMMIT TRAN claim_tran;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN claim_tran;
	END CATCH
END


/*----- FOR CHECKING-----*/
/*-- USP_CLAIM_TRANSACTION_ENTRY @UserId INT, @ClaimId INT --*/
EXEC USP_CLAIM_TRANSACTION_ENTRY 1,1

/*----- PROC 9 -----*/
/*-----GET CLAIM TRANSACTION DATA -----*/
CREATE PROCEDURE USP_GET_CLAIMS_TRANSACTION_DATA
@UserId INT
AS
BEGIN
	SELECT
		emt.Transaction_No,
		cm.Claim_Title,
		cm.Claim_Reason,
		cm.Amount,
		cm.Claim_Description,
		cm.ClaimDt,
		emt.TransactionDt,
		um.Nm ApprovedBy
		FROM Employee_Claim_Transaction(NOLOCK) emt
		INNER JOIN Claim_Master(NOLOCK) cm
		ON emt.CliamId = cm.Id
		INNER JOIN User_Master(NOLOCK) um
		on emt.Employee_Id = um.Id
		WHERE cm.UserId = @UserId; 
END

/*----- FOR CHECKING-----*/
/*-- USP_GET_CLAIMS_TRANSACTION_DATA @UserId INT--*/
EXEC USP_GET_CLAIMS_TRANSACTION_DATA 2


/*----- PROC 10 -----*/
/*----- USER MANAGE -----*/
CREATE PROCEDURE USP_MANAGED_USER
@Action VARCHAR(20), --- This action like Create/Update/Get ---
@Id INT = NULL,
@Name VARCHAR(100) = NULL,
@Email VARCHAR(100) = NULL,
@Mobile VARCHAR(100) = NULL,
@Password VARCHAR(100) = NULL,
@Manager INT = NULL,
@Status TINYINT = NULL
AS
BEGIN
	IF @Action = 'create' AND EXISTS (SELECT 1 FROM User_Master WHERE Email = @Email)
		BEGIN
			THROW 50000, 'User Email Already Exist', 1;
			RETURN;
		END
	IF @Action = 'update' AND EXISTS (SELECT 1 FROM User_Master WHERE Email = @Email AND Id <> @Id)
		BEGIN
			THROW 50000, 'User Email Already Exist For Another Someone', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_userManage
			IF @Action = 'create'
				BEGIN
					INSERT INTO User_Master (
											Nm,
											Email,
											Mobile,
											Password,
											Manager_Id,
											Status
											)VALUES(
											@Name,
											@Email,
											@Mobile,
											DBO.HashPassword(@Password),
											@Manager,
											@Status);
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'update'
				BEGIN
					UPDATE User_Master SET
										Nm = @Name,
										Email = @Email,
										Mobile = @Mobile,
										Manager_Id = @Manager,
										Status = @Status
										WHERE Id = @Id;
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'get'
				BEGIN
					SELECT
						Id, Nm Name, Email, Mobile, Manager_Id, Status
						FROM User_Master(NOLOCK)
						WHERE Id = @Id;
				END
			ELSE IF @Action = 'getall'
				BEGIN
					SELECT
						U1.Id,
						U1.Nm Name,
						U1.Email,
						U1.Mobile,
						ISNull(U2.Nm,'Not Assigned') Manager,
						CASE WHEN U1.Status = 1 THEN 'Active'
							ELSE 'Inactive'
						END Status
						FROM User_Master(NOLOCK) U1
						LEFT JOIN User_Master(NOLOCK) U2
						ON U1.Manager_Id = U2.Id
						WHERE U1.Id<>@Id;
				END
		COMMIT TRAN trn_userManage;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_userManage;
	END CATCH
END


/*----- FOR CHECKING-----*/
/*--- CREATE ---*/
/* EXEC USP_MANAGED_USER @Action='create',
					  @Name = 'Sunil Sir',
					  @Email = 'sun123@gmail.com',
					  @Mobile = '1234567890',
					  @Password = 'sun@123',
					  @Manager = 1,
					  @Status = 1;
*/
/*--- UPDATE ---*/
/* EXEC USP_MANAGED_USER @Action='update',
					  @Id = 6,
					  @Name = 'Sunil Sir',
					  @Email = 'sunilSir123@gmail.com',
					  @Mobile = '1234567890',
					  @Manager = 1,
					  @Status = 1;
*/
/*--- GET ---*/
/* EXEC USP_MANAGED_USER @Action='get',
					   @Id = 6
*/
/*--- GETALL ---*/
/* EXEC USP_MANAGED_USER @Action='getall',
					  @Id = 3
*/		



/*----- PROC 11 -----*/
/*----- GET USER THROUGH BY ROLE -----*/
CREATE PROCEDURE USP_GET_USER_BY_ROLE
@Role VARCHAR(50),
@Id INT
AS
BEGIN
	SELECT
		um.Id, um.Nm Name
		FROM User_Master um
		INNER JOIN Role_Employee_Mapping rem
		ON um.Id = rem.EmpId
		INNER JOIN Role_Master rm
		ON rem.RoleId = rm.Id
		WHERE rm.Role = @Role
		AND um.Id<>@Id AND um.Status = 1;
END


/*----- FOR CHECKING-----*/
EXEC USP_GET_USER_BY_ROLE 'Manager',2;



/*----- PROC 12 -----*/
/*----- ROLE MANAGE -----*/
CREATE PROCEDURE USP_MANAGED_ROLE
@Action VARCHAR(50),
@Id INT = NULL,
@RoleName VARCHAR(50) =  NULL,
@Status TINYINT = NULL
AS
BEGIN
	IF @Action = 'create' AND EXISTS (SELECT 1 FROM Role_Master WHERE Role = @RoleName)
		BEGIN
			THROW 50000, 'This Role Already Exist', 1;
			RETURN;
		END
	IF @Action = 'update' AND EXISTS (SELECT 1 FROM Role_Master WHERE Role = @RoleName AND Id <> @Id)
		BEGIN
			THROW 50000, 'This Role Already Exist', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_role
			IF @Action = 'create'
				BEGIN
					INSERT INTO Role_Master(Role, Status)
									VALUES(@RoleName, @Status);
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'update'
				BEGIN
					UPDATE Role_Master SET
								Role = @RoleName,
								Status = @Status
								WHERE Id = @Id;
			SELECT 1 AS RESULT;
				END
			ELSE IF @Action ='getall'
				BEGIN
					SELECT Id, Role,
					CASE WHEN Status = 1 THEN 'Active'
							ELSE 'Inactive' 
					END Status
					FROM Role_Master(NOLOCK);
				END
			ELSE IF @Action ='get'
				BEGIN
					SELECT Id, Role, Status FROM Role_Master(NOLOCK) WHERE Id = @Id;
				END
		COMMIT TRAN trn_role;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_role;
	END CATCH
END


/*----- FOR CHECKING-----*/
/*--- CREATE ---*/
/* EXEC USP_MANAGED_ROLE @Action='create',
					  @RoleName = 'Super Admin',
					  @Status = 1;
*/
/*--- UPDATE ---*/
/* EXEC USP_MANAGED_ROLE @Action='update',
					  @RoleName = 'Manager',
					  @Status = 1,
					  @Id = 1;
	*/
/*--- GET ---*/
/* EXEC USP_MANAGED_ROLE @Action='get',
					   @Id = 2
*/
/*--- GETALL ---*/
/* EXEC USP_MANAGED_ROLE @Action='getall'
*/