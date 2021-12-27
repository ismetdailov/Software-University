CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
	DECLARE @EmployeeDep INT = (SELECT E.DepartmentId
								FROM Employees AS E
								WHERE E.Id = @EmployeeId)

	DECLARE @Report INT = (SELECT C.DepartmentId
						  FROM Reports AS R
						   JOIN Categories AS C ON C.Id = R.CategoryId
						  WHERE R.Id = @ReportId )

	IF @EmployeeDep != @Report
	THROW 50005,'Employee doesn''t belong to the appropriate department!', 1;
	
UPDATE Reports 
SET EmployeeId = @EmployeeId
	WHERE Id = @ReportId
GO
