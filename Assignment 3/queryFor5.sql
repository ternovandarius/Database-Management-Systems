use Ternovan_Lab
go

--this procedure does the things required for grade 5:
--it tries to add a commercial, a commercial actor and their id's in the m:n table
--however, as the procedure calls are in different transactions, if one doesn't work, it won't roll the others back
create or alter procedure addCommercialCommercialActorSaving (@product varchar(50), @runtime int, @name varchar(50), @age int, @nr_of_takes int)
as
	declare @cid int
	declare @caid int
	begin tran
		begin try
			exec addCommercial @product, @runtime;
			set @cid = (select IDENT_CURRENT('Commercials'))
			commit tran
		end try
	begin catch
		rollback tran
		print 'addCommercial rolled back'
	end catch
	
	begin tran
		begin try
			exec addCommercialActor @name, @age;
			set @caid = (select IDENT_CURRENT('CommercialActors'))
			commit tran
		end try
	begin catch
		rollback tran
		print 'addCommercialActor rolled back'
	end catch

	begin tran
		begin try
			if @nr_of_takes <= 0
				raiserror('Nr of takes cannot be lower or equal to 0!', 14, 1)
			insert into CommercialsCommercialActors values (@nr_of_takes, @cid, @caid)

			commit tran
			print 'M:N transaction commited'
		end try
	begin catch
		rollback tran
		print 'M:N transaction rolled back'
	end catch
go

exec addCommercialCommercialActorSaving 'idkk', -15, 'idk', 39, 25;