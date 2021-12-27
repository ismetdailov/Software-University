SELECT TOP(5) C.Name AS CategoryName,
		COUNT(*) AS ReportsNumber
 FROM Categories C
 JOIN Reports R ON C.Id = R.CategoryId
 GROUP BY C.Name, R.CategoryId
 ORDER BY ReportsNumber DESC , CategoryName