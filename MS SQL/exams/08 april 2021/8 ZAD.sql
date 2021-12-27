SELECT E.FirstName+' '+E.LastName AS FullName,
		 COUNT(DISTINCT R.UserId) AS UserCount
FROM Employees E 
JOIN Reports R ON E.Id =R.EmployeeId
GROUP BY E.FirstName,E.LastName,E.Id
ORDER BY UserCount DESC , FullName