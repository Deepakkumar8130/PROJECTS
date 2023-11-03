
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
					SELECT 1 AS RESULT
				END
			ELSE
				BEGIN
					SELECT 2 AS RESULT
				END
		END
	ELSE
		BEGIN
			SELECT 3 AS RESULT
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
	ORDER BY prog.Display_Sequence
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
	SELECT Id, Nm, Email, Mobile, Manager_Id, Status
		FROM User_Master(NOLOCK)
		WHERE Email = @email
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
@ExpenseDt DATETIME,
@Claim_Description VARCHAR(500)
AS
BEGIN
	DECLARE @Current_Status VARCHAR(100)
	DECLARE @ClaimId INT
	SELECT @Current_Status =NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = 'Initiated'
	BEGIN TRAN Trn_Claim
		BEGIN TRY
		/*CHECK CLAIM TABLE USER ALREADY RAISED CLAIM OR NOT*/
		IF EXISTS (SELECT 1 FROM Claim_Master(NOLOCK) WHERE UserId = @UserId AND CurrentStatus LIKE '%Pending%')
			BEGIN
			RAISERROR('Claim Already In Pending',1,1)
			RETURN
			END
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
		COMMIT TRAN Trn_Cliam
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN Trn_Claim
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
	SELECT  cm.Id,
			um.Nm,
			cm.Claim_Title,
			cm.Claim_Reason,
			cm.Amount,
			cm.ClaimDt,
			cm.Evidence,
			cm.ExpenseDt,
			cm.Claim_Description,
			cm.CurrentStatus
			FROM Claim_Master(NOLOCK) cm
			INNER JOIN User_Master(NOLOCK) um
			ON cm.UserId = um.Id
			WHERE cm.CurrentStatus = (SELECT rm.Action FROM Employee_Claim_Role_Master(NOLOCK) rm
												WHERE rm.Role = @role)
			AND um.Manager_Id = CASE WHEN @role = 'Manager'
										THEN @userId
										ELSE um.Manager_Id
								END
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
	DECLARE @current_status VARCHAR(100)
	DECLARE @next_action VARCHAR(100)

	SELECT @current_status = CurrentStatus FROM Claim_Master(NOLOCK)
							 WHERE Id = @claimId
	
	SELECT @next_action = NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = @current_status AND Status = @action
	BEGIN TRAN Trn_Update_Claim
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
										)
		COMMIT TRAN Trn_Update_Claim
		END TRY
		BEGIN CATCH
		ROLLBACK TRAN Trn_Update_Claim
		END CATCH
END


/*----- FOR CHECKING-----*/
/*EXEC USP_UPDATE_CLAIM @claimId INT, @action TINYINT, @userId INT, @role VARCHAR(20), @remark VARCHAR(200)*/
EXEC USP_UPDATE_CLAIM 1,1,1,'Manager','APPROVE BY MANAGER'
---------
EXEC USP_UPDATE_CLAIM 1,1,4,'HR','APPROVE BY HR'
