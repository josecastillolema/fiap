create database if not exists fiapdb;
use fiapdb;
create table if not exists Aso (id INT(6) PRIMARY KEY, name VARCHAR(30) NOT NULL);
insert into Aso values (1234, "Jose Castillo Lema");
