

/*----- ADD ROLES ------*/
INSERT INTO Role_Master VALUES
('Employee',1),
('Manager',1),
('HR',1),
('Account',1),
('Admin',1);

/*----- ADD EMPLOYEES ------*/
INSERT INTO User_Master (
	Nm,
	Email,
	Mobile,
	Password,
	Manager_Id,
	Status
) VALUES
('Akash Kumar','akash123@gmail.com','93483943',DBO.HashPassword('aks@123'),NULL,1),
('Rahul Kumar','rahul123@gmail.com','73483943',DBO.HashPassword('rh@123'),NULL,1),
('Mayank Kumar','mayank0023@gmail.com','88483943',DBO.HashPassword('mank@123'),NULL,1),
('Sumit Kumar','smt03@gmail.com','78483943',DBO.HashPassword('smt@123'),NULL,1),
('Pawan Kumar','pawan123@gmail.com','87483943',DBO.HashPassword('pawan@123'),NULL,1),
('Sunil Sir','sunilSir123@gmail.com','87483943',DBO.HashPassword('sun@123'),NULL,1);


/*----- ASIGN MANAGERS ------*/
UPDATE User_Master SET Manager_Id = 1 WHERE Id = 2
UPDATE User_Master SET Manager_Id = 1 WHERE Id = 3
UPDATE User_Master SET Manager_Id = 3 WHERE Id = 4
UPDATE User_Master SET Manager_Id = 3 WHERE Id = 5


/*----- ASIGN ROLES ------*/
INSERT INTO Role_Employee_Mapping (
	RoleId,
	EmpId,
	Status
) VALUES
(1,2,1),
(2,3,1),
(2,1,1),
(3,4,1),
(4,5,1),
(5,6,1);


/*----- ADD PROGRAMS ------*/
INSERT INTO Program_Master (
	P_Title,
	Path,
	Descr,
	Display_Sequence,
	Status
) VALUES
('Add Claim','Claim/AddClaim','Add new claim',0,1),
('Employee Claims','Claim/ShowClaim','show claim request',1,1),
('Dashboard','Home/Dashboard','dashboard',2,1),
('Show Claim Status','Claim/ClaimStatus','Show Claim Status',3,1),
('Claim Transactions','Claim/ShowClaimTransaction','Show Claims Transaction',3,1),
('Managed User','User/ManageUser','Users Managed',4,1),
('Managed Roles','Role/ManageRole','Roles Managed',5,1),
('Assign Role','Role/AssignRole','Assigned Role To User',6,1),
('Assign Individual Rights','ProgramRights/AssignRights','Assigned Program Rights',7,1),
('Assign Group Rights','ProgramRights/AssignGroupRights','Assigned Program Rights For Group',8,1);


/*----- ASIGN PROGRAMS RIGHTS ------*/
INSERT INTO Tbl_Rights(Programe_id,UserId,RoleId,Status)
VALUES
/*--- EMPLOYEE ---*/
(1,NULL,1,1),
(3,NULL,1,1),
(4,NULL,1,1),
(5,NULL,1,1),
/*--- MANAGER ---*/
(1,NULL,2,1),
(2,NULL,2,1),
(3,NULL,2,1),
(4,NULL,2,1),
(5,NULL,2,1),
/*--- HR ---*/
(1,NULL,3,1),
(2,NULL,3,1),
(3,NULL,3,1),
(4,NULL,3,1),
(5,NULL,3,1),
/*--- ACCOUNT ---*/
(1,NULL,4,1),
(2,NULL,4,1),
(3,NULL,4,1),
(4,NULL,4,1),
(5,NULL,4,1),
/*--- ADMIN ---*/
(1,NULL,5,1),
(2,NULL,5,1),
(3,NULL,5,1),
(4,NULL,5,1),
(5,NULL,5,1),
(6,NULL,5,1),
(7,NULL,5,1),
/*--- SUPER ADMIN ---*/
(1,NULL,6,1),
(2,NULL,6,1),
(3,NULL,6,1),
(4,NULL,6,1),
(5,NULL,6,1),
(6,NULL,6,1),
(7,NULL,6,1),
(8,NULL,6,1),
(9,NULL,6,1);




/*----- ASSIGN ROLE STATUS -----*/
INSERT INTO Employee_Claim_Role_Master(Role, Action, Status)
VALUES
('Employee', 'Initiated', 1),
('Manager', 'Pending At Manager', 1),
('HR', 'Pending At HR', 1),
('Account', 'Pending At Account', 1);



/*----- ASSIGN CLAIM STATUS CYCLE MAPPING -----*/
INSERT INTO Employee_Claim_Master_Mapping(CurrentAction, NextAction, Status)
VALUES
('Initiated', 'Pending At Manager', 1),
('Pending At Manager', 'Pending At HR', 1),
('Pending At Manager', 'Rejected By Manager', 0),
('Pending At HR', 'Pending At Account', 1),
('Pending At HR', 'Rejected At HR', 0),
('Pending At Account', 'Completed', 1);


/*----- Query checks -----*/
SELECT pro.P_Title,emp.Nm, emp.Id
	FROM Program_Master pro
	INNER JOIN	Tbl_Rights tbl
	ON pro.Id = tbl.Programe_id
	INNER JOIN User_Master emp
	ON emp.Id = tbl.UserId;

