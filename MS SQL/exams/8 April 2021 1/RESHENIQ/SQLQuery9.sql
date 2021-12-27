SELECT TOP(5) C.Name AS CategoryName , COUNT(*) AS ReportsNumbers
FROM Reports R
JOIN Categories C ON R.CategoryId = C.Id
GROUP BY C.Name
ORDER BY ReportsNumbers DESC, C.Name