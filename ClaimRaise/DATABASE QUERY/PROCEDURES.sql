
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
	OR tbL.RoleId IN (SELECT Id FROM Role_Employee_Mapping(NOLOCK) WHERE EmpId = @userId)
	ORDER BY prog.Display_Sequence;
END

/*----- FOR CHECKING-----*/
EXEC USP_GET_PROGRAMS 1
---------
EXEC USP_GET_PROGRAMS 2


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



/*----- PROC 6 -----*/
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



/*----- PROC 7 -----*/
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

/*----- PROC 8 -----*/
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