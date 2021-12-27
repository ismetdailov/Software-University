CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME) 
RETURNS INT
AS
BEGIN
		DECLARE @RESULT INT =(SELECT DATEDIFF(HOUR,OpenDate,CloseDate) AS TotalHour
						FROM Reports R
						WHERE @StartDate = OpenDate AND @EndDate =CloseDate);
						IF @RESULT IS NULL
						SET @RESULT = 0
	RETURN @RESULT
END
GO