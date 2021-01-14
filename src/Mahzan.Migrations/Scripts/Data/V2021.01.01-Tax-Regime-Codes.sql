/*
	Table Name:		Codigos SAT de Regimen Fiscal
	Descripción:	Contiene el catalgos de los regimen fiscal de las personas Fisicas y Morales.
*/

insert into tax_regime_codes as current (tax_regime_code_id,code,description,moral_person,physical_person) 
values('09ACA4EE-02D6-468C-9606-1515A12AEB04','601','General de Lay Personas Morales',false, true)
on conflict(tax_regime_code_id)
	do update 
		set description = excluded.description
		where current.description <> excluded.description;