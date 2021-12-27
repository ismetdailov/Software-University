CREATE FUNCTION udf_FlightDestinationsByEmail(@email VARCHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @COC INT
		SET @COC=(SELECT  COUNT(*) AS Countair
FROM Passengers P
JOIN FlightDestinations FD ON P.Id = FD.PassengerId
WHERE P.Email =@email
GROUP BY FD.PassengerId, P.FullName)
		IF(@COC IS NULL) SET @COC= 0
		
RETURN @COC

END