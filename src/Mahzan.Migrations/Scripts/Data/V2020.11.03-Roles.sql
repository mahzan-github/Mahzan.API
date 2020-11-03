/*
	Table Name:		Roles
	Descripción:	Contiene el catalogo de roles que nacen con la apliación
*/

insert into roles as current (role_id,"name") 
values('fb4b765a-7fb9-4293-a548-924f6fc6dfb2','MEMBER')
on conflict(role_id)
	do update 
		set name = excluded.name
		where current.name <> excluded.name