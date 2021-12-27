SELECT R.Description AS Description,
		C.Name AS CategoryName
FROM Reports R
JOIN Categories C ON R.CategoryId = C.Id
ORDER BY R.Description , C.Name