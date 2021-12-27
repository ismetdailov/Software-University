CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
		DECLARE @RESSULT INT = (SELECT *
											FROM Reports R
												JOIN Categories C ON R.CategoryId = C.DepartmentId
												JOIN Employees E ON R.EmployeeId=E.DepartmentId
												WHERE E.DepartmentId=@EmployeeId AND C.DepartmentId = @ReportId);
												IF @EmployeeId !=@ReportId
										THROW 50001,'Employee doesn ''t belong to the appropriate department!',1
											IF @RESSULT = NULL
										THROW 50001,'Employee doesn ''t belong to the appropriate department!',1
										UPDATE Reports
										SET EmployeeId = @EmployeeId
										WHERE Id = @ReportId
END