/**
	Key Managment Install Script
	Developed by: Zach Justus
	Org: Luminate Church
	Date: 2/15/2020
**/
/** create tbales **/
BEGIN
Use ArenaDB


--drop table cust_luminate_key_door_key
--drop table cust_luminate_key_person_key
--drop table cust_luminate_key_door
--drop table cust_luminate_key_key
--drop table cust_luminate_key_location;

Create table dbo.cust_luminate_key_location(
	location_id int not null Primary key IDENTITY(1,1),
	parent_id int foreign key references cust_luminate_key_location(location_id),
	location_name varchar(255)
)
create table dbo.cust_luminate_key_door(
	door_id int not null primary key identity(1,1),
	location_id int foreign key references cust_luminate_key_location(location_id) ON DELETE CASCADE,
	door_name varchar(255)
)
create table dbo.cust_luminate_key_key(
	[key_id] int not null primary key identity(1,1),
	[key_name] varchar(255)
)
create table dbo.cust_luminate_key_door_key(
	door_key_id int not null primary key identity(1,1),
	[key_id] int not null foreign key references cust_luminate_key_key([key_id]) ON DELETE CASCADE,
	door_id int not null foreign key references cust_luminate_key_door(door_id) ON DELETE CASCADE
)
create table dbo.cust_luminate_key_person_key(
	person_key_id int not null primary key identity(1,1),
	person_id int not null foreign key references core_person(person_id) ON DELETE CASCADE,
	[key_id] int not null foreign key references cust_luminate_key_key([key_id]) ON DELETE CASCADE,
	date_issued date
)
END
go
/** create stored procedures **/
--add location
create or alter proc dbo.cust_luminate_key_sp_add_location(@location_name varchar(255), @parent_id int = NULL, @location_id int output) as
BEGIN
	insert into dbo.cust_luminate_key_location(location_name, parent_id)
		Values(@location_name, @parent_id)

	set @location_id = @@IDENTITY
	return
END
GO
--add door
create or alter proc dbo.cust_luminate_key_sp_add_door(@location_id int, @door_name varchar(255), @door_id int output) as
BEGIN
	insert into dbo.cust_luminate_key_door(location_id, door_name)
		values(@location_id, @door_name)

	set @door_id = @@IDENTITY
	return
END
GO
--add key
create or alter proc dbo.cust_luminate_key_sp_add_key(@key_name varchar(255), @key_id int output) as
BEGIN
	insert into dbo.cust_luminate_key_key([key_name])
		values(@key_name)

	set @key_id = @@IDENTITY
	return
END
GO
--add door key
create or alter proc dbo.cust_luminate_key_sp_add_door_key(@key_id int, @door_id int, @door_key int output) as
BEGIN
	insert into dbo.cust_luminate_key_door_key([key_id], door_id)
		values(@key_id, @door_id)

	set @door_key = @@IDENTITY
	return
END
GO
--add person key
create or alter proc dbo.cust_luminate_key_sp_add_person_key(@person_id int, @key_id int, @key_person int output) as
BEGIN
	insert into dbo.cust_luminate_key_person_key(person_id, key_id, date_issued)
	values(@person_id, @key_id, getdate())
END
GO
--del location
create or alter proc dbo.cust_luminate_key_sp_del_location(@location_id int) as
BEGIN
	delete from dbo.cust_luminate_key_location where location_id = @location_id
END
GO
--del door
create or alter proc dbo.cust_luminate_key_sp_del_door(@door_id int) as
BEGIN
	delete from dbo.cust_luminate_key_door where door_id = @door_id
END
GO
--del key
create or alter proc dbo.cust_luminate_key_sp_del_key(@key_id int) as
BEGIN
	delete from dbo.cust_luminate_key_key where [key_id] = @key_id
END
GO
--del key_person
create or alter proc dbo.cust_luminate_key_sp_del_key_person(@person_key_id int) as
BEGIN
	delete from dbo.cust_luminate_key_person_key where person_key_id = @person_key_id
END
GO
--del door_key
create or alter proc dbo.cust_luminate_key_sp_del_door_key(@door_key_id int) as
BEGIN
	delete from dbo.cust_luminate_key_door_key where door_key_id = @door_key_id
END
GO
--get keys
create or alter proc dbo.cust_luminate_key_sp_get_keys as
BEGIN
	select * from dbo.cust_luminate_key_key
END
GO
--get locations
create or alter proc dbo.cust_luminate_key_sp_get_locations(@parent_id int = null) as
BEGIN
	if @parent_id is null
		select * from dbo.cust_luminate_key_location where parent_id is null
	else
		select * from dbo.cust_luminate_key_location where parent_id = @parent_id
END
GO
--get doors
create or alter proc dbo.cust_luminate_key_sp_get_doors(@location_id int) as
BEGIN
	select * from dbo.cust_luminate_key_door where location_id = @location_id
END
GO
--get person keys
create or alter proc dbo.cust_luminate_key_sp_get_person_keys(@person_id int) as
BEGIN
	select
		[key].[key_id],
		[key].[key_name],
		key_person.date_issued
	from dbo.cust_luminate_key_key as [key]
	left join dbo.cust_luminate_key_person_key as key_person on [key].key_id = key_person.key_id
	where key_person.person_id = @person_id
END
GO
--get key holders
create or alter proc dbo.cust_luminate_key_sp_get_key_persons(@key_id int) as
BEGIN
	select
		person.person_id,
		person.first_name,
		person.last_name
	from dbo.core_person as person
	left join dbo.cust_luminate_key_person_key as key_person on person.person_id = key_person.person_id
	where key_person.[key_id] = @key_id
END
GO
--get key doors
create or alter proc dbo.cust_luminate_key_sp_get_key_doors(@key_id int) as
BEGIN
	select
		door_key.door_key_id,
		door.door_id,
		door.door_name
	from dbo.cust_luminate_key_door as door
	left join dbo.cust_luminate_key_door_key as door_key on door.door_id = door_key.door_id
	where door_key.[key_id] = @key_id
END
GO
--get door key
create or alter proc dbo.cust_luminate_key_sp_get_door_keys(@door_id int) as
BEGIN
	select
		door_key.door_key_id,
		[key].[key_id],
		[key].[key_name]
	from dbo.cust_luminate_key_key as [key]
	left join dbo.cust_luminate_key_door_key as door_key on [key].[key_id] = door_key.[key_id]
	where door_key.door_id = @door_id
END
GO
-- update location name
CREATE or ALTER proc [dbo].[cust_luminate_key_sp_update_location] @location_id int, @location_name varchar(255) as
begin
	update dbo.cust_luminate_key_location
	set location_name = @location_name
	where location_id = @location_id
end
GO
-- update door name
CREATE or ALTER proc [dbo].[cust_luminate_key_sp_update_door] @door_id int, @door_name varchar(255) as
begin
	update dbo.cust_luminate_key_door
	set door_name = @door_name
	where door_id= @door_id
end
GO
