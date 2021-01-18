select * from construction_cost
select * from cities
select * from building_functions
select * from quality_functions

insert into construction_cost(name,year,city_id,func_id,price_min,price_max)values(@name,@year,@city_id,@func_id,@price_min,@price_max)


select bf.name as function_name,qf.quality_name as quality_name,c.name as city_name, cc.price_max, cc.price_min,cc.year，c.lat, c.lon
FROM construction_cost cc, cities c, building_functions bf, quality_functions qf
where cc.city_id=c.code and cc.func_id=bf.id and cc.quality_id=qf.quality_id;

update construction_cost  set price_max = replace(price_min , -1.00 , 0)

alter table construction_cost alter column price_min int

create table quality_functions(
	quality_id serial  primary key,
	quality_name char(40)
);

select * from quality_functions

INSERT INTO quality_functions(quality_name) VALUES
	('high_quality'),
	('medium'),
	('low'),
	('clubhouse'),
	('external_work'),
	('5_star'),
	('3_star'),
	('landlord'),
	('end_user'),
	('basement'),
	('multi_story');

select distinct on(name) * from construction_cost;

ALTER TABLE construction_cost ADD quality_name_id int;

select * from construction_cost

ALTER TABLE construction_cost Drop name;

alter table construction_cost MODIFY name  after id;

