--As my database was about television, I decided to create new tables on the theme of commercials
--My reasoning for the choices: the names of the products or actors are not primary keys, as a product can have multiple commercials, and there can be
--multiple actors with the same name.
--The tables are in a m:n relationship, as a commercial can have multiple actors, and an actor can appear in multiple commercials.

create table Commercials (
	cid int primary key identity (1, 1),
	product varchar(50) not null,
	runtime int not null,
)

create table CommercialActors (
	caid int primary key identity (1, 1),
	name varchar(50) not null,
	age int not null,
)

create table CommercialsCommercialActors (
	nr_of_takes int not null,
	cid int foreign key references Commercials(cid),
	caid int foreign key references CommercialActors(caid),
	primary key (cid, caid)
)