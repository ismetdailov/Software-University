SELECT R.Description, C.Name 
FROM Reports R
JOIN Categories C ON R.CategoryId = C.Id
ORDER BY R.Description,C.Name
