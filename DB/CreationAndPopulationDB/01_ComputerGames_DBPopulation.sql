use ComputerGames
--DB population
INSERT INTO Publisher (PublisherName,LicenseNumber) VALUES ('Ubisoft',123456), ('Electronic Arts',124365),('Valve',654321),('Blizzard',125634);
INSERT INTO Genre (GenreName,Description) VALUES ('Shooter','kill them with fire and michigun'), ('Sport simulator','footbal, hockey, car racing and etc'), 
('Strategy','make your own kingdom')
--DELETE FROM Genre
--DELETE FROM Publisher
INSERT INTO Game (GameName,YearOfProduction,GenreId,PublisherID) VALUES ('Half Life',1996,1,3),('Counter Strike',1997,1,3),('FIFA',1995,2,2),
('Need for speed',1998,2,2),('Starcraft','1996',3,4),('Far cry',2002,1,1),('Heroes of Might and magic VI',2006,3,1)
--DELETE FROM Game


--for test
SELECT GameName,PublisherName FROM Game gm 
LEFT JOIN Publisher pbl
ON gm.PublisherID=pbl.Id
WHERE gm.PublisherID=(SELECT pb.Id FROM Publisher pb WHERE pb.PublisherName='Ubisoft')

SELECT * FROM Publisher

SELECT *  FROM Game gm INNER JOIN Publisher pb ON gm.PublisherID = pb.Id INNER JOIN Genre gn ON gm.GenreId=gn.Id WHERE pb.LicenseNumber = 124365 
SELECT *FROM Publisher pb INNER JOIN Game gm on pb.Id=gm.PublisherID where pb.LicenseNumber=124365

var sql = $"SELECT * FROM Shoes s INNER JOIN Humans h on s.Id = h.ShoesId WHERE s.Id = {id};";