SELECT TOP(5) R.Id, R.Name, COUNT(*) AS Commits
FROM Repositories R
JOIN Commits C ON R.Id =C.RepositoryId
JOIN RepositoriesContributors RC ON R.Id =RC.RepositoryId
GROUP BY R.Id ,R.Name 
ORDER BY Commits DESC, R.Id,R.Name