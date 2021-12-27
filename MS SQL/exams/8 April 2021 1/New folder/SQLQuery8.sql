SELECT U.Username AS Username,
		C.Name AS CategoryName
FROM Users U 
JOIN Reports R ON U.Id = R.UserId
JOIN Categories C ON R.CategoryId = C.Id
WHERE DAY(U.Birthdate) = DAY(R.OpenDate) AND MONTH(U.Birthdate) =MONTH(R.OpenDate)
ORDER BY Username, CategoryName