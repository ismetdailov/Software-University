SELECT E.FirstName+' '+E.LastName AS FullName, COUNT(*) AS UserCount
		 
FROM Employees E 
JOIN Reports R ON E.Id =R.EmployeeId
JOIN Users U ON R.EmployeeId = U.Id
WHERE  E.Id =R.UserId 

GROUP BY E.FirstName,E.LastName,E.Id,R.UserId
ORDER BY UserCount DESC , FullName