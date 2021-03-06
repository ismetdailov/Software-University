SELECT FI.Id, FI.Name, CONCAT(FI.Size,'KB') AS Size
FROM Files F
RIGHT JOIN Files FI ON F.ParentId =FI.Id
WHERE F.ParentId IS NULL
ORDER BY FI.Id, FI.Name, FI.Size DESC
