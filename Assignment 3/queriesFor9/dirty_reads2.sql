use Ternovan_Lab
go

--to solve this problem, simply change "uncommitted" to "committed"

set tran isolation level read uncommitted

begin tran
select * from Commercials
waitfor delay '00:00:10'
select * from Commercials
commit tran