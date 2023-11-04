USE PROJECT4;

SELECT*FROM sys.procedures
sp_helptext USP_GET_CUSTOMER

CREATE PROC USP_GET_COUNTRY
AS
BEGIN
	SELECT ID,NAME FROM country(NOLOCK)
END
GO

CREATE PROC USP_GET_STATE
@ID INT
AS
BEGIN
	SELECT ID,NAME FROM state(NOLOCK) WHERE country_id = @ID
END
GO

CREATE PROC USP_GET_CITY
@ID INT
AS
BEGIN
	SELECT ID,NAME FROM city(NOLOCK) WHERE state_id=@ID
END


CREATE PROC USP_GET_CUSTOMERS  
AS  
 BEGIN  
	  SELECT  
		  CUST.id Id,  
		  CUST.Name,  
		  CUST.Mobile,  
		  CUST.Email,  
		  CUST.gender Gender,  
		  C.name Country,  
		  S.name State,  
		  CTY.name City 
		  
		  FROM  customer(NOLOCK) CUST  
		  LEFT JOIN country C  
		  ON CUST.country = C.id  
		  LEFT JOIN state(NOLOCK) S  
		  ON CUST.state = S.id  
		  LEFT JOIN city(NOLOCK) CTY  
		  ON CUST.city = CTY.id  
		  WHERE isActive = 1  
 END


CREATE PROC USP_SAVE_CUSTOMER(
@name VARCHAR(50),
@email VARCHAR(100),
@mobile VARCHAR(20),
@gender VARCHAR(10),
@country INT,
@state INT,
@city INT
)
AS
BEGIN
	IF(NOT EXISTS(SELECT 1 FROM customer where email=@email))
	BEGIN
		INSERT INTO customer(
			Name,  
			Email,  
			Mobile,  
			gender,
			country,
			state,
			city,
			isActive
			) VALUES(
			 @name,
			 @email,
			 @mobile,
			 @gender,
			 @country,
			 @state,
			 @city,
			 1)
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
	END
END


 CREATE PROC USP_PERMENENT_DELETE_CUSTOMER  
@id INT  
AS  
 BEGIN  
	 IF(EXISTS (SELECT 1 FROM customer WHERE id = @id))  
		 BEGIN  
			  DELETE   
			  FROM customer  
			  WHERE id = @id  
			  SELECT 1 AS RESULT  
		 END  
	 ELSE  
		 BEGIN  
		  SELECT 3 AS RESULT  
		 END  
 END

GO

CREATE PROC USP_DELETE_CUSTOMER  
@id INT  
AS  
 BEGIN  
	 IF(EXISTS (SELECT 1 FROM customer WHERE id = @id))  
		 BEGIN  
			  UPDATE customer  
			  SET  
			   isActive = 0  
			   WHERE id = @id  
			   SELECT 1 AS RESULT  
		 END  
	 ELSE  
		 BEGIN  
		  SELECT 3 AS RESULT  
		 END  
 END






CREATE PROC USP_GET_CUSTOMER(
@id INT
)
AS
BEGIN
	SELECT
	  id Id,  
	  Name,  
	  Mobile,  
	  Email,  
	  gender Gender,  
	  country Country,  
	  state State,  
	  city City  
	  customer(NOLOCK) WHERE id = @id AND isActive = 1  
END

CREATE PROC USP_UPDATE_CUSTOMER  
@id INT,  
@name VARCHAR(20),  
@email VARCHAR(100),  
@mobile VARCHAR(20),  
@gender VARCHAR(10),  
@country INT,  
@state INT,  
@city INT  
AS  
 BEGIN  
	 IF(EXISTS (SELECT 1 FROM customer WHERE id = @id))  
		 BEGIN  
			  UPDATE customer  
			  SET  
			   Name = @name,  
			   Email = @email,  
			   Mobile = @mobile,  
			   gender = @gender,  
			   country = @country,  
			   state = @state,  
			   city = @city  
			   WHERE id = @id  
			   SELECT 1 AS RESULT  
		 END  
	 ELSE  
		 BEGIN  
			SELECT 3 AS RESULT  
		 END  
 END

EXEC USP_GET_COUNTRY;
EXEC USP_GET_STATE 1;
EXEC USP_GET_CITY 1;
EXEC USP_GET_CUSTOMERS;
EXEC USP_SAVE_CUSTOMER 'Test23','Test23@gmail.com','1234567890','Male',1,1,1;
EXEC USP_DELETE_CUSTOMER 1;
EXEC USP_GET_CUSTOMER 2;
EXEC USP_UPDATE_CUSTOMER 2,'Test','Test@gmail.com','1234567890','Male',1,1,1;
select*from customer



  
 CREATE PROC USP_UNDO_CUSTOMER  
@id INT  
AS  
 BEGIN  
	 IF(EXISTS (SELECT 1 FROM customer WHERE id = @id))  
		 BEGIN  
			  UPDATE customer  
			  SET  
			   isActive = 1  
			   WHERE id = @id  
			   SELECT 1 AS RESULT  
		 END  
	 ELSE  
		 BEGIN  
		  SELECT 4 AS RESULT  
		 END  
 END

EXEC USP_GET_COUNTRY;
EXEC USP_GET_STATE 1;
EXEC USP_GET_CITY 1;
EXEC USP_GET_CUSTOMERS;
EXEC USP_SAVE_CUSTOMER 'Test23','Test23@gmail.com','1234567890','Male',1,1,1;
EXEC USP_DELETE_CUSTOMER 1;
EXEC USP_GET_CUSTOMER 2;
EXEC USP_UPDATE_CUSTOMER 2,'Test','Test@gmail.com','1234567890','Male',1,1,1;
EXEC USP_GET_BINCUSTOMERS 

select*from sys.tables

GO

CREATE PROC UPDATE_PROFILE_IMAGE  
@id INT,  
@path VARCHAR(500)  
AS  
BEGIN  
	 INSERT INTO profile_image(  
		  customer_id,  
		  path)  
		  VALUES(  
		  @id,  
		  @path)  
END



CREATE PROC USP_GET_BINCUSTOMERS  
AS  
 BEGIN  
  SELECT  
	  CUST.id Id,  
	  CUST.Name,  
	  CUST.Mobile,  
	  CUST.Email,  
	  CUST.gender Gender,  
	  C.name Country,  
	  S.name State,  
	  CTY.name City  
	  FROM  customer(NOLOCK) CUST  
	  LEFT JOIN country C  
	  ON CUST.country = C.id  
	  LEFT JOIN state(NOLOCK) S  
	  ON CUST.state = S.id  
	  LEFT JOIN city(NOLOCK) CTY  
	  ON CUST.city = CTY.id  
	  WHERE isActive = 0  
 END


sp_helptext USP_GET_COUNTRY
sp_helptext USP_GET_CUSTOMERS
sp_helptext USP_GET_STATE
sp_helptext USP_GET_CITY
sp_helptext USP_SAVE_CUSTOMER
sp_helptext USP_GET_CUSTOMER
sp_helptext USP_UPDATE_CUSTOMER
sp_helptext USP_DELETE_CUSTOMER
sp_helptext USP_GET_BINCUSTOMERS
sp_helptext USP_UNDO_CUSTOMER
sp_helptext USP_PERMENENT_DELETE_CUSTOMER
sp_helptext UPDATE_PROFILE_IMAGE
