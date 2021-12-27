SELECT*, E.FirstName+' '+E.LastName AS FullName
			--COUNT(*) AS UsersCount
FROM Employees E
LEFT JOIN Reports R ON E.Id = R.Id
LEFT JOIN Users U ON U.Id = E.Id
GROUP BY E.FirstName,E.LastName, U.Username
SELECT E.FirstName+' '+E.LastName AS FullName,
			COUNT(*) AS UsersCount
FROM Employees E
LEFT JOIN Reports R ON E.Id = R.Id
LEFT JOIN Users U ON U.Id = E.Id

GROUP BY E.FirstName,E.LastName, E.Id
ORDER BY UsersCount DESC , FullName