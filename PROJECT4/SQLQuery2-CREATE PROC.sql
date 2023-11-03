USE PROJECT4;

SELECT*FROM sys.procedures

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
	cust.id,
	cust.name,
	cust.email,
	cust.mobile,
	cust.gender,
	cnty.name country,
	st.name state,
	cty.name city
	
	FROM customer(NOLOCK) cust
	LEFT JOIN
	country(NOLOCK) cnty ON cust.country_id = cnty.id
	LEFT JOIN
	state(NOLOCK) st ON cust.state_id =st.id
	LEFT JOIN
	city(NOLOCK) cty ON cust.city_id = cty.id
	WHERE isActive=1;
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
			name,
			email,
			mobile,
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


CREATE PROC USP_PERMENENET_DELETE_CUSTOMER(
@id INT
)
AS
BEGIN
	IF(EXISTS (SELECT 1 FROM customer where id = @id))
	BEGIN
		DELETE FROM customer WHERE id = @id
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
	END
END

GO

CREATE PROC USP_DELETE_CUSTOMER(
@id INT
)
AS
BEGIN
	IF(EXISTS (SELECT 1 FROM customer where id = @id))
	BEGIN
		UPDATE FROM customer
		SET
		isActive = 0
		WHERE id = @id
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
	END
END






CREATE PROC USP_GET_CUSTOMER(
@id INT
)
AS
BEGIN
	SELECT
	id,
	name,
	email,
	mobile,
	gender,
	country_id,
	state_id,
	city_id
	FROM customer where id = @id AND isActive=1
END

CREATE PROC USP_UPDATE_CUSTOMER(
@id INT,
@name VARCHAR(50),
@email VARCHAR(100),
@mobile VARCHAR(20),
@gender VARCHAR(10),
@country_id INT,
@state_id INT,
@city_id INT
)
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM customer where email=@email))
	BEGIN
		UPDATE customer
			SET 
			name= @name,
			email=@email,
			mobile= @mobile,
			gender= @gender,
			country_id=@country_id,
			state_id=@state_id,
			city_id=@city_id
			WHERE id = @id
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
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



CREATE PROC USP_DELETE_CUSTOMER(
@id INT
)
AS
BEGIN
	IF(EXISTS (SELECT 1 FROM customer where id = @id))
	BEGIN
		UPDATE FROM customer
		SET
		isActive = 0
		WHERE id = @id
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
	END
END






CREATE PROC USP_GET_CUSTOMER(
@id INT
)
AS
BEGIN
	SELECT
	id,
	name,
	email,
	mobile,
	gender,
	country_id,
	state_id,
	city_id
	FROM customer where id = @id AND isActive=1
END

CREATE PROC USP_UNDO_CUSTOMER(
@id INT,
@name VARCHAR(50),
@email VARCHAR(100),
@mobile VARCHAR(20),
@gender VARCHAR(10),
@country_id INT,
@state_id INT,
@city_id INT
)
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM customer where email=@email))
	BEGIN
		UPDATE customer
			SET 
			isActive=1
		SELECT 1 AS RESULT
	END
	ELSE
	BEGIN
		SELECT 2 AS RESULT
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


