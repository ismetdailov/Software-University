SELECT TOP(5) (C.Name) , COUNT(*) AS ReportNumbers
		
		
FROM Categories C
JOIN Reports R ON C.Id = R.CategoryId
GROUP BY  C.Name
ORDER BY ReportNumbers DESC, C.Name
