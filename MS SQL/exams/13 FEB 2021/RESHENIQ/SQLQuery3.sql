DELETE FROM RepositoriesContributors
WHERE RepositoryId = (SELECT Repositories.Id
FROM Repositories
WHERE Name = 'Softuni-Teamwork')

DELETE FROM Issues
WHERE RepositoryId =(SELECT Repositories.Id
FROM Repositories
WHERE Name = 'Softuni-Teamwork')

