SELECT C.Id,C.Message,C.RepositoryId,C.ContributorId
FROM Commits AS C
ORDER BY C.Id,C.Message,C.RepositoryId,C.ContributorId
 