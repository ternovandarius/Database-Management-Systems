use Ternovan_Lab
go

--read committed for issue
--repeatable read for solution

--When a row has different values between queries

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

begin tran
SELECT TOP (3) *
FROM CommercialActors
order by name

waitfor delay '00:00:10'

SELECT TOP (3) *
FROM CommercialActors
order by name

commit tran