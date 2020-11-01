/*
    Descripción: Creación de estructura inicial de base de datos
*/

/*[Users]*/
create table if not exists users
(
    user_id                 uuid            NOT NULL,
    user_name               varchar(50)     NOT NULL,
    password                varchar(50)     NOT NULL,
    active                  boolean         NOT NULL,
    confirm_email           uuid            NULL,
    token_confirm_email     boolean         NULL,
    user_pattern_id         uuid            NULL,
    email                   varchar(50)      NOT NULL,
    created_at              timestamp       NULL,
    last_login_at           timestamp       NULL,
    PRIMARY KEY (user_id)
);