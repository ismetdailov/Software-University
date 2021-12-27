SELECT Description,
      C.Name AS CategoryName
FROM Reports R
JOIN Categories C ON C.Id = R.CategoryId
ORDER BY Description, C.Name
