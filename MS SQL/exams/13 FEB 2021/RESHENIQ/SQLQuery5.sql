SELECT F.Id, F.Name,F.Size
FROM Files F
WHERE F.Size > 1000 AND F.Name LIKE '%html%'
ORDER BY F.Size DESC, F.Name