insert into client (id, name, surname, age, passport, phone_number, date_of_birth)
values
(uuid_generate_v4(), 'Maria', 'Demchenco', 33, 1234, 373789476, '1997-09-02'),
(uuid_generate_v4(), 'Mihail', 'Voronin', 45, 4568, 373741159, '1980-02-15'),
(uuid_generate_v4(), 'Lena', 'Mihina', 18, 7415, 373963147, '2005-06-06');

insert  into employee (name, surname, age, passport, phone_number, date_of_birth, contract, salary)
values
('Alice', 'Johnson', 28, 8520, 373023614, '1995-08-12', 'Full-Time', 50000),
('Bob', 'Williams', 35, 1289, 373741963, '1988-11-05', 'Part-Time', 30000),
('Charles', 'Davis', 45, 0178, 373027961, '1978-05-03', 'Contract', 70000);

INSERT INTO account (amount, "currency_id ", "client_id ")
VALUES
(1, '83fd2091-eeec-4df2-aa16-b6bfd202cad7', 'ba2c1141-a337-49c0-bbea-38fa349a071c'),
(2, 'e13a7456-cb07-4d26-b3eb-7850343967f0', 'a9229f72-ebdf-48ca-85b7-fe3597d4148a'),
(3, '83fd2091-eeec-4df2-aa16-b6bfd202cad7', '04e9edb3-449a-42d9-9ca5-195cf2eb2520'),
(1, 'e13a7456-cb07-4d26-b3eb-7850343967f0', '83fd2091-eeec-4df2-aa16-b6bfd202cad7');

select name, surname, amount from client
join account on client.id = "client_id "
where amount<600 order by amount;

select name, surname, amount from  client
join account a on client.id = a."client_id "
order by amount limit 1;

select sum(amount) as sum_amount from account;

select name, surname, amount,currency_name from client
join account a on client.id = a."client_id "
join currency c on a."currency_id " = c.id;

select name, surname, age from client
order by age DESC;

select age, count(*) as same_age_people from client
group by age
having count(*)>1;

select age, count(*) as same_age_people from client
group by age;

select * from client
limit n;