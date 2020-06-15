use Ternovan_Lab
go

set deadlock_priority low;

begin tran
update CommercialActors set name='maybe i dk' where age>20
waitfor delay '00:00:10'
update Commercials set product='maybe i dk' where runtime = 30
commit tran