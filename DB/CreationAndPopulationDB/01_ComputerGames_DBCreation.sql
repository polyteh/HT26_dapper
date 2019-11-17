create database ComputerGames
--drop database ComputerGames
use ComputerGames

create table Publisher
(
	Id int Identity(1,1) primary key,
	PublisherName nvarchar(100) unique not null,
	LicenseNumber int unique not null
)
--drop table Publisher

create table Genre
(
	Id int Identity(1,1) primary key,
	GenreName nvarchar(60) unique not null,
	Description nvarchar(600)
)
--drop table Genre
create table Game
(
	Id int Identity(1,1) primary key,
	GameName nvarchar(60) not null,
	YearOfProduction int not null,
	GenreId int not null,
	PublisherID int not null	
)
--drop table Game
ALTER TABLE Game ADD CONSTRAINT UK_NameYear UNIQUE (GameName,YearOfProduction)
ALTER TABLE Game WITH CHECK ADD FOREIGN KEY(GenreId) REFERENCES Genre (Id)
ALTER TABLE Game WITH CHECK ADD FOREIGN KEY(PublisherID) REFERENCES Publisher (Id)