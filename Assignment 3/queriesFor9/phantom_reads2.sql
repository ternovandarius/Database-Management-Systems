use Ternovan_Lab
go

begin tran
insert into CommercialActors values ('Aaron', 40)
commit tran

delete from CommercialActors where name='Aaron'