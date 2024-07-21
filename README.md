# Proyecto Angular

## Descripción

Este es un proyecto de aplicación Angular que se conecta a una API y utiliza una base de datos SQL Server para gestionar usuarios y personas.

## Requisitos

- Node.js (v14 o superior)
- Angular CLI
- SQL Server

## Configuración de la Base de Datos

Para configurar la base de datos, sigue estos pasos:

1. **Ejecutar el Script de la Base de Datos**

   Ejecuta el siguiente script SQL en tu instancia de SQL Server para crear la base de datos, esquemas y tablas necesarios:

   ```sql
   CREATE DATABASE DoublePartners;
   USE DoublePartners;

   CREATE SCHEMA Auth;

   CREATE TABLE Auth.Users (
       UserID INT IDENTITY(1,1) PRIMARY KEY,
       UserName NVARCHAR(50) UNIQUE NOT NULL,
       UserPassWord NVARCHAR(255) NOT NULL
   );

   INSERT INTO Auth.Users(UserName, UserPassWord) VALUES ('test', 'test');

   CREATE SCHEMA Persons;

   CREATE TABLE Persons.Persons (
       IdPerson INT IDENTITY(1,1) PRIMARY KEY,
       FirstName NVARCHAR(100) NOT NULL,
       LastName NVARCHAR(100) NOT NULL,
       IdentificationNumber NVARCHAR(50) NOT NULL,
       Email NVARCHAR(255) NOT NULL,
       IdentificationType NVARCHAR(50) NOT NULL,
       CreationDate DATETIME DEFAULT GETDATE(),
       FullIdentification AS (IdentificationNumber + '-' + IdentificationType),
       FullName AS (FirstName + ' ' + LastName)
   );

   CREATE PROCEDURE GetPersons
   AS
   BEGIN
       SELECT
           IdPerson,
           FirstName,
           LastName,
           IdentificationNumber,
           Email,
           IdentificationType,
           CreationDate,
           FullIdentification,
           FullName
       FROM Persons.Persons;
   END;

