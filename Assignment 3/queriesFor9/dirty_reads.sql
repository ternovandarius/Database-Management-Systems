use Ternovan_Lab
go

--a dirty read is when a table is read during a transaction, then rows in that table are modified by another transaction which is not committed,
--then read again, it will have the uncommitted values in the second read, even though the modifying transaction may have been rolled back

begin tran
update Commercials set product='updated' where runtime=15
waitfor delay '00:00:10'
rollback tran