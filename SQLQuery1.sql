CREATE DATABASE Filmes_Gabriel

USE Filmes_Gabriel

CREATE TABLE Genero
(
	IdGenero INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) NOT NULL
)

CREATE TABLE Filme
(
	IdFilme INT PRIMARY KEY IDENTITY,
	IdGenero INT FOREIGN KEY REFERENCES Genero(IdGenero),
	Titulo VARCHAR(50) NOT NULL
)
INSERT INTO Genero
VALUES
('Ação'),('Comédia'), ('Terror')

SELECT * FROM Genero

INSERT INTO Filme
VALUES
(1,'Missão Impossivel'),
(2,'Golpe Baixo'),
(3, 'Hereditário')

SELECT * FROM Filme