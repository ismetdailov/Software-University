SELECT 
			U.Username ,
			C.Name AS CategoryName
FROM Users U
JOIN Reports R ON U.Id =R.UserId
JOIN Categories C ON R.CategoryId = C.Id
WHERE MONTH(R.OpenDate) =MONTH(U.Birthdate) AND DAY(R.OpenDate)= DAY(U.Birthdate)
ORDER BY U.Username, C.Name