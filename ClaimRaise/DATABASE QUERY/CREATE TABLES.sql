CREATE DATABASE ClaimDB;

GO;

USE ClaimDB;

CREATE TABLE User_Master
(	Id INT PRIMARY KEY IDENTITY,
	Nm VARCHAR(50),
	Email VARCHAR(100),
	Mobile VARCHAR(50),
	Password VARCHAR(500),
	Manager_Id INT,
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Program_Master
(	Id INT PRIMARY KEY IDENTITY,
	P_Title VARCHAR(100),
	Path VARCHAR(500),
	Descr VARCHAR(500),
	Display_Sequence INT,
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Role_Master
(	Id INT PRIMARY KEY IDENTITY,
	Role VARCHAR(100),
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Role_Employee_Mapping
(	Id INT PRIMARY KEY IDENTITY,
	RoleId INT,
	EmpId INT,
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Tbl_Rights
(	Id INT PRIMARY KEY IDENTITY,
	Programe_id INT,
	UserId INT,
	RoleId INT,
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Claim_Master
(	Id INT PRIMARY KEY IDENTITY,
	UserId INT,
	Claim_Title VARCHAR(100),
	Claim_Reason VARCHAR(100),
	Amount DECIMAL,
	ClaimDt DATETIME,
	Evidence VARCHAR(500),
	ExpenseDt DATETIME,
	Claim_Description VARCHAR(500),
	CurrentStatus VARCHAR(50),
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Employee_Claim_Role_Master
(	Id INT PRIMARY KEY IDENTITY,
	Role VARCHAR(100),
	Action VARCHAR(100),
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Employee_Claim_Master_Mapping
(	Id INT PRIMARY KEY IDENTITY,
	CurrentAction VARCHAR(100),
	NextAction VARCHAR(100),
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Employee_Claim_Transaction
(	Id INT PRIMARY KEY IDENTITY,
	Transaction_No VARCHAR(100),
	Employee_Id INT,
	Amount DECIMAL,
	TransactionDt DATETIME,
	ClaimId INT,
	Status TINYINT
);

/*---------------------------------------*/

CREATE TABLE Employee_Claim_Action
(	Id INT PRIMARY KEY IDENTITY,
	ClaimId INT,
	Action VARCHAR(100),
	ActionBy INT,
	ActionDt DATETIME,
	Remarks VARCHAR(100),
	Status TINYINT
);
