SELECT *FROM Reports
WHERE CloseDate IS NULL

UPDATE Reports SET CloseDate = '2021-11-21'
WHERE CloseDate IS NULL