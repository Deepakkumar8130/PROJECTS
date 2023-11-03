CREATE DATABASE PROJECT4;

USE PROJECT4;

CREATE TABLE country(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(20)
)

GO

CREATE TABLE state(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(20),
	country_id INT FOREIGN KEY REFERENCES country(id)
)

GO

CREATE TABLE city(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(20),
	state_id INT FOREIGN KEY REFERENCES state(id)
)

GO

CREATE TABLE customer(
	id INT IDENTITY,
	Name VARCHAR(20),
	Email  VARCHAR(20),
	Mobile VARCHAR(20),
	gender varchar(20),
	country INT,
	state INT,
	city INT,
	isActive varchar(2)
)


alter table customer add isActive varchar(2);

select*from profile_image


CREATE TABLE profile_image(
id INT IDENTITY,
customer_id INT,
path VARCHAR(500)
);