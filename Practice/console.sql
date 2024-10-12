CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create table client
(
    id            uuid default uuid_generate_v4() not null
        constraint client_pk
            primary key,
    name          text                            not null,
    surname       text                            not null,
    age           integer                         not null,
    passport      integer                         not null,
    phone_number  integer                         not null,
    date_of_birth date                            not null
);

alter table client
    owner to postgres;

create unique index client_id_uindex
    on client (id);

create unique index client_passport_uindex
    on client (passport);

create unique index client_phone_number_uindex
    on client (phone_number);

create table account
(
    id             uuid default uuid_generate_v4() not null
        constraint account_pk
            primary key,
    amount         numeric,
    "currency_id " uuid                            not null
        constraint currency_id
            references currency
            on update cascade on delete cascade,
    "client_id "   uuid
        constraint client_id
            references client
            on update cascade on delete cascade,
    "employee_id " uuid
        constraint employee_id
            references employee
            on update cascade on delete cascade
);

alter table account
    owner to postgres;

create unique index account_id_uindex
    on account (id);

create table currency
(
    id            uuid default uuid_generate_v4() not null
        constraint currency_pk
            primary key,
    currency_name text                            not null,
    code          text                            not null
);

alter table currency
    owner to postgres;

create unique index currency_code_uindex
    on currency (code);

create unique index currency_id_uindex
    on currency (id);

create unique index currency_name_uindex
    on currency (currency_name);