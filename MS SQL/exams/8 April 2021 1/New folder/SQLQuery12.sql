CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT) 
AS
BEGIN 
DECLARE @report INT = (SELECT C.DepartmentId 
			FROM Reports R
			JOIN Categories C ON R.CategoryId = C.Id
			WHERE R.Id = @ReportId)
	DECLARE  @emplo INT =(SELECT DepartmentId FROM Employees  WHERE @EmployeeId = Id)
			IF( @report != @emplo)
			THROW 50002, 'Employee doesn''t belong to the appropriate department!', 1;
			UPDATE Reports
			SET EmployeeId = @EmployeeId
			WHERE Id = @ReportId
			END