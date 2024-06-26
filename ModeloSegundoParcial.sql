USE ModeloSegundoParcial
CREATE TABLE frutas
(
	id int identity(1,1) not null primary key,
	nombre varchar(100), 
	peso int,
	precio float

);

INSERT INTO frutas (nombre, peso, precio)
VALUES ('Manzana', 150, 0.75),
       ('Banana', 120, 0.50),
       ('Naranja', 180, 0.60),
       ('Pera', 170, 0.80);