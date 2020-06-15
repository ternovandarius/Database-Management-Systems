use Ternovan_Lab
go

begin tran
update CommercialActors set age=50 where name = 'Aaron'
commit tran