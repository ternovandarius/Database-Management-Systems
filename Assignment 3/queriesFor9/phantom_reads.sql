use Ternovan_Lab
go

--When between identical queries, different rows are returned

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

begin tran
SELECT TOP (3) *
FROM CommercialActors
order by name

waitfor delay '00:00:10'

SELECT TOP (3) *
FROM CommercialActors
order by name

commit tran