use Ternovan_Lab
go

--this procedure adds a commercial, only if the product name is not empty, and the runtime is >0
create or alter procedure addCommercial (@product varchar(50), @runtime int)
as
	begin tran
		begin try
			if @product is null
				raiserror('Product name cannot be empty!', 14, 1)
			if @runtime <= 0
				raiserror('Runtime cannot be lower or equal to 0!', 14, 1)
			insert into Commercials values (@product, @runtime)
			commit tran
			print 'Commercial transaction commited'
		end try
	begin catch
		rollback tran
		print 'Commercial transaction rolled back'
	end catch
go

--this procedure adds a commercial actor, only if the actor name is not empty, and their age is >0
create or alter procedure addCommercialActor (@name varchar(50), @age int)
as
	begin tran
		begin try
			if @name is null
				raiserror('Actor name cannot be empty!', 14, 1)
			if @age <=0
				raiserror('Actor age cannot be lower or equal to 0!', 14, 1)
			insert into CommercialActors values (@name, @age);

			commit tran
			print 'CommercialActor transaction commited'
		end try
	begin catch
		rollback tran
		print 'CommercialActor transaction rolled back'
	end catch
go

--this procedure does what is required for grade 3:
--it tries to add a commercial, then a commercial actor, then get their primary keys, and use them to insert data into the m:n table
--however, as they are in the same transaction, if one fails, all of them will be rolled back
create or alter procedure addCommercialCommercialActor (@product varchar(50), @runtime int, @name varchar(50), @age int, @nr_of_takes int)
as
	begin tran
		begin try
			exec addCommercial @product, @runtime;
			exec addCommercialActor @name, @age;
			if @nr_of_takes <= 0
				raiserror('Nr of takes cannot be lower or equal to 0!', 14, 1)

			declare @cid int
			set @cid = (select IDENT_CURRENT('Commercials'))
			declare @caid int
			set @caid = (select IDENT_CURRENT('CommercialActors'))

			print @cid
			print @caid

			insert into CommercialsCommercialActors values (@nr_of_takes, @cid, @caid)

			commit tran
			print 'M:N transaction commited'
		end try
	begin catch
		rollback tran
		print 'M:N transaction rolled back'
	end catch
go

exec addCommercial 'iPhone XXSR', -30;

exec addCommercialActor 'John Doe', 25;
exec addCommercialActor 'Jane Doe', -23;

exec addCommercialCommercialActor 'ASDF', 35, 'Rob', 33, 50;