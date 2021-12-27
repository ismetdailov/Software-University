CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME) 
RETURNS INT
AS
BEGIN
  DECLARE @Result INT 
			IF	@StartDate IS NULL OR @EndDate IS NULL
			BEGIN
			  SET @Result = 0;
			END
			  ELSE 
			BEGIN 
			  SET @Result = DATEDIFF(HOUR,@StartDate, @EndDate)
			END
			RETURN @Result;
 END
GO

