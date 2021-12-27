SELECT I.Id, U.Username+' : '+I.Title AS IssueAssignee
FROM Issues I
 JOIN Users U ON I.AssigneeId = U.Id
 ORDER BY I.Id DESC, I.AssigneeId