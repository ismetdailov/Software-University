SELECT  E.FirstName+' '+E.LastName AS FullName,
	COUNT(DISTINCT R.UserId) AS UsersCount
FROM Reports R
RIGHT JOIN Employees E ON R.EmployeeId = E.Id
GROUP BY  E.FirstName,E.LastName,R.EmployeeId
ORDER BY UsersCount DESC , FullName