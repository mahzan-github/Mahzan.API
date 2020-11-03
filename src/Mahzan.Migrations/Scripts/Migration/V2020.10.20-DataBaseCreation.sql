/*
    Descripción: Creación de estructura inicial de base de datos
*/

/*  Table Name:     Users
    Description:    Contiene los usuarios de la aplicación
*/
create table if not exists users
(
    user_id                 uuid            NOT NULL,
    user_name               varchar(50)     NOT NULL,
    password                varchar(50)     NOT NULL,
    active                  boolean         NOT NULL,
    confirm_email           boolean         NULL,
    token_confirm_email     uuid            NULL,
    user_pattern_id         uuid            NULL,
    email                   varchar(50)     NOT NULL,
    created_at              timestamp       NULL,
    last_login_at           timestamp       NULL,
    PRIMARY KEY (user_id)
);

/*  Table Name:     Members
    Description:    Contiene la información de los miembros de la aplaición
*/
create table if not exists members
(
    member_id                 uuid            NOT NULL,
    "name"                    varchar(50)     NOT NULL,
    phone                     varchar(18)     NOT NULL,
    user_id                   uuid            NOT NULL,
    PRIMARY KEY (member_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

/*  Table Name:     Roles
    Description:    Contiene los roles de los uaurios de la aplicación
*/
create table if not exists roles
(
    role_id                 uuid            NOT NULL,
    "name"                  varchar(50)     NOT NULL,
    PRIMARY KEY (role_id)
);

/*  Table Name:     User Role
    Description:    Contiene los roles asignado a un usuario.
*/
create table if not exists user_role
(
    user_id                 uuid            NOT NULL,
    role_id                 uuid     NOT NULL,

    PRIMARY KEY (user_id,role_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
);