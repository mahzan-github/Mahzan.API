/*
	Table Name:		Menu Sections
	Descripción:	Contiene las secctiones del Menú
*/
insert into menu_sections as current (menu_section_id,section)
values('61f520e1-53ac-4474-a5c0-5bc2cc5b2354','Configuración')
on conflict(menu_section_id)
    do update
           set name = excluded.name
       where current.name <> excluded.name;

/*
	Table Name:		Menu Sections
	Descripción:	Contiene las secctiones del Menú
*/
insert into menu_selections as current (menu_selection_id,title,root,icon,menu_section_id)
values('aa4d46db-505d-4ad7-84d7-8d73ed0a4d8d','Empresa','true')
on conflict(menu_section_id)
    do update
           set name = excluded.name
       where current.name <> excluded.name;

/*
	Table Name:		Menu Roles
	Descripción:	Contiene el catalogo de roles que nacen con la apliación
*/