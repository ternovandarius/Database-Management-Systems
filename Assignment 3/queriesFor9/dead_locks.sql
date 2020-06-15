use Ternovan_Lab
go

--deadlocks are when two transactions are trying to modify the same table at the same time
--from what I understood, it is impossible to know which transaction the db will sacrifice
--to solve this, we can set the deadlock_priority to high, low or medium (or a numeric value),
--to be able to control which transaction gets rolled back in case of a deadlock

set deadlock_priority high;

begin tran
update Commercials set product='idk' where runtime=30
waitfor delay '00:00:10'
update CommercialActors set name='idk' where age<30
commit tran