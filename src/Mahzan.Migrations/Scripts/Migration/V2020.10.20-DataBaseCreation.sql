/*
    Descripción: Creación de estructura inicial de base de datos
*/

/*  Table Name:     Users
    Description:    Contiene los usuarios de la aplicación
*/
create table if not exists "users"
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
create table if not exists "members"
(
    member_id                 uuid            NOT NULL,
    "name"                    varchar(50)     NOT NULL,
    phone                     varchar(19)     NOT NULL,
    user_id                   uuid            NOT NULL,
    member__pattern_id        uuid            NULL,
    PRIMARY KEY (member_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
    );

/*  Table Name:     Licencia de Miembros
    Description:    Contiene la información de los miembros de la aplaición
*/
create table if not exists "members_license"
(
    member_license_id         uuid            NOT NULL,
    license_type              varchar(50)     NOT NULL,
    created_at                timestamp       NULL,
    start_license_at          timestamp       NULL,
    end_license_at            timestamp       NULL,
    member_id                 uuid            NOT NULL,
    PRIMARY KEY (member_license_id)
    --FOREIGN KEY (member_id) REFERENCES members(member_id)
    );

/*  Table Name:     Roles
    Description:    Contiene los roles de los uaurios de la aplicación
*/
create table if not exists "roles"
(
    role_id                 uuid            NOT NULL,
    "name"                  varchar(50)     NOT NULL,
    PRIMARY KEY (role_id)
    );

/*  Table Name:     User Role
    Description:    Contiene los roles asignado a un usuario.
*/
create table if not exists "user_role"
(
    user_id                 uuid            NOT NULL,
    role_id                 uuid            NOT NULL,

    PRIMARY KEY (user_id,role_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
    );

/*  Table Name:     Company Adress
    Description:    Contiene la dirección de una compañia
*/
create table if not exists "tax_regime_codes"
(
    tax_regime_code_id      uuid            NOT NULL,
    code                    varchar(3)      NOT NULL,
    description             varchar(250)    NOT NULL,
    moral_person            boolean         NULL,
    physical_person         boolean         NULL,
    PRIMARY KEY (tax_regime_code_id)
    );

/*  Table Name:     Companies
    Description:    Contiene las compañias de un usuario
*/
create table if not exists "companies"
(
    company_id              uuid            NOT NULL,
    rfc                     varchar(13)     NOT NULL,
    curp                    varchar(18)     NULL,
    comercial_name          varchar(50)     NOT NULL,
    business_name           varchar(50)     NOT NULL,
    email                   varchar(50)     NOT NULL,
    active                  boolean         NOT NULL,
    member_id               uuid            NOT NULL,
    tax_regime_code_id      uuid            NULL, -- Codigo de Regimen Fiscal
    office_phone            varchar(18)     NULL,
    mobile_phone            varchar(18)     NULL,
    additional_information  varchar(500)    NULL,

    PRIMARY KEY (company_id),
    FOREIGN KEY (member_id) REFERENCES members(member_id),
    FOREIGN KEY (tax_regime_code_id) REFERENCES tax_regime_codes(tax_regime_code_id)
    );


/*  Table Name:     Company Adress
    Description:    Contiene la dirección de una compañia
*/
create table if not exists "companies_addresses"
(
    company_adress_id       uuid            NOT NULL,
    adress_type             varchar(25)     NOT NULL, --FISCAL_LOCATION,EXPEDITION_PLACE
    street                  varchar(50)     NOT NULL,
    exterior_number         varchar(25)     NOT NULL,
    internal_number         varchar(25)     NULL,
    postal_code             varchar(5)      NOT NULL,
    company_id              uuid            NOT NULL,

    PRIMARY KEY (company_adress_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Postal Codes (Sepomex)
    Description:    Contiene los códigos postales
*/
create table if not exists "postal_codes"
(
    postal_code_id          uuid            NOT NULL,

    PRIMARY KEY (postal_code_id)
    );


/*  Table Name:     Events
    Description:    Contiene los códigos postales
*/
create table if not exists "events_log"
(
    event_log_id            uuid            NOT NULL,
    controller              varchar(25)     NOT NULL,
    "action"                varchar(25)     NOT NULL,
    old_value               json            NULL,
    new_value               json            NULL,
    event_at                timestamp       NOT NULL,
    user_id                 uuid            NOT NULL,
    user_name               varchar(50)     NOT NULL,
    PRIMARY KEY (event_log_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
    );

/*  Table Name:     Bank Accounts
    Description:    Contiene las cuentas bancarias de una compañia
*/
create table if not exists "bank_accounts"
(
    bank_account_id         uuid            NOT NULL,
    account                 varchar(50)     NOT NULL,
    branch_office           varchar(50)     NOT NULL, -- Sucursal
    clabe                   varchar(50)     NOT NULL,
    bank                    varchar(50)     NOT NULL,
    accounting_account      varchar(50)     NOT NULL,
    company_id              uuid            NOT NULL,
    PRIMARY KEY (bank_account_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Product Categories
    Description:    Contiene las categorias a las que se pueden relacionar los productos
*/
create table if not exists "product_catagories"
(
    product_catagory_id     uuid            NOT NULL,
    code_category           varchar(25)     NULL,
    description             varchar(50)     NOT NULL,
    company_id              uuid            NOT NULL,
    PRIMARY KEY (product_catagory_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Product Departments
    Description:    Contiene los departamentos a las que se pueden relacionar los productos
*/
create table if not exists "product_departments"
(
    product_department_id   uuid            NOT NULL,
    code_department         varchar(25)     NULL,
    "name"                  varchar(50)     NOT NULL,
    company_id              uuid            NOT NULL,
    PRIMARY KEY (product_department_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Product Purchase Units
    Description:    Contiene las unidades de compra de los productos
*/
create table if not exists "product_purchase_units"
(
    product_purchase_unit_id    uuid            NOT NULL,
    abbreviation                varchar(25)     NULL,
    description                 varchar(50)     NOT NULL,
    company_id                  uuid            NOT NULL,
    PRIMARY KEY (product_purchase_unit_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Product sale Units
    Description:    Contiene las unidades de venta de los productos
*/
create table if not exists "product_sale_units"
(
    product_sale_unit_id        uuid            NOT NULL,
    abbreviation                varchar(25)     NULL,
    description                 varchar(50)     NOT NULL,
    company_id                  uuid            NOT NULL,
    PRIMARY KEY (product_sale_unit_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Product Taxes
    Description:    Contiene los posibles impuestos aplicables a los productos
*/
create table if not exists "product_taxes"
(
    product_tax_id              uuid            NOT NULL,
    "name"                      varchar(25)     NOT NULL,
    percentage                  float           NOT NULL,
    print_on_ticket             boolean         NOT NULL,
    company_id                  uuid            NOT NULL,
    PRIMARY KEY (product_tax_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );

/*  Table Name:     Products
    Description:    Contiene los productos
*/
create table if not exists "products"
(
    product_id                  uuid            NOT NULL,
    key_code                    varchar(25)     NULL,
    key_alternative_code        varchar(25)     NULL,
    description                 varchar(100)    NOT NULL,
    product_catagory_id         uuid            NULL,
    product_department_id       uuid            NULL,
    product_purchase_unit_id    uuid            NULL,
    product_sale_unit_id        uuid            NULL,
    factor                      numeric(5,2)    NULL,
    company_id                  uuid            NOT NULL,
    PRIMARY KEY (product_id),
    FOREIGN KEY (company_id) REFERENCES companies(company_id)
    );
/*  Table Name:     Product Sale Taxes
    Description:    Contiene los impuestos que serán aplicados al articulo
                    al momento de la venta.
*/
create table if not exists "product_sale_taxes"
(
    product_sale_tax_id         uuid            NOT NULL,
    product_tax_id              uuid            NOT NULL,
    product_id                  uuid            NOT NULL,
    PRIMARY KEY (product_sale_tax_id),
    FOREIGN KEY (product_tax_id) REFERENCES product_taxes(product_tax_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
    );

/*  Table Name:     Product Sale Prices
    Description:    Contiene los diferentes precios que pueden ser asignado
                    al momento de la venta
*/
create table if not exists "product_sale_prices"
(
    product_sale_price_id               uuid            NOT NULL,
    price_type                          varchar(25)     NOT NULL,  --PUBLICO, MAYOREO, MENUDEO
    price                               numeric(7,2)    NOT NULL,
    cost                                numeric(7,2)    NULL,
    utility                             numeric(7,2)    NULL,
    utility_percentage                  numeric(4,2)    NULL,
    product_id                          uuid            NOT NULL,
    PRIMARY KEY (product_sale_price_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
    );

/*  Table Name:     Menu Sections
    Description:    Contiene las posibles secciones del menú
*/
create table if not exists "menu_sections"
(
    menu_section_id                         uuid            NOT NULL,
    "section"                               varchar(25)     NOT NULL,
    PRIMARY KEY (menu_section_id)
    );

/*  Table Name:     Menu de Role
    Description:    Contiene el menú de un role que será visible en la aplicacion web
*/
create table if not exists "menu_role"
(
    menu_role_id                            uuid            NOT NULL,
    menu_section_id                         uuid            NULL,
    menu_selection_id                       uuid            NULL,
    menu_sub_menu_id                        uuid            NULL,
    menu_sub_menu_item_id                   uuid            NULL,
    "order"                                 int             NOT NULL,
    role_id                                 uuid            NOT NULL,
    PRIMARY KEY (menu_role_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (menu_section_id) REFERENCES menu_sections(menu_section_id)
    );

/*  Table Name:     Menu Selections
    Description:    Contiene las posibles selecciones de la seccion
*/
create table if not exists "menu_selections"
(
    menu_selection_id                           uuid            NOT NULL,
    "title"                                     varchar(25)     NOT NULL,
    root                                        boolean         NOT NULL,
    icon                                        varchar(50)     NOT NULL,
    menu_section_id                             uuid            NOT NULL,
    PRIMARY KEY (menu_selection_id),
    FOREIGN KEY (menu_section_id) REFERENCES menu_sections(menu_section_id)
    );

/*  Table Name:     Menu Sub Menu
    Description:    Contiene las posibles menus de la selección
*/
create table if not exists "menu_sub_menu"
(
    menu_sub_menu_id                         uuid            NOT NULL,
    "title"                                  varchar(25)     NOT NULL,
    page                                     varchar(50)     NOT NULL,
    bullet                                   boolean         NOT NULL,
    menu_selection_id                          uuid            NOT NULL,
    PRIMARY KEY (menu_sub_menu_id),
    FOREIGN KEY (menu_selection_id) REFERENCES menu_selections(menu_selection_id)
    );

/*  Table Name:     Menu Sub Menu Items
    Description:    Contiene las posibles items del sub menu
*/
create table if not exists "menu_sub_menu_items"
(
    menu_sub_menu_item_id                    uuid            NOT NULL,
    "title"                                  varchar(25)     NOT NULL,
    page                                     varchar(25)     NOT NULL,
    menu_sub_menu_id                         uuid            NOT NULL,
    PRIMARY KEY (menu_sub_menu_item_id),
    FOREIGN KEY (menu_sub_menu_id) REFERENCES menu_sub_menu(menu_sub_menu_id)
    );

/*  Table Name:     Clients
    Description:    Contiene los datos de un cliente
*/
create table if not exists "clients"
(
    client_id                               uuid            NOT NULL,
    client_number                           varchar(50)     NULL,
    client_code                             varchar(50)     NULL,
    name_agent                              varchar(100)    NOT NULL,   --Nombre de Representante Legal
    name                                    varchar(100)    NOT NULL,
    rfc                                     varchar(13)     NULL,
    curp                                    varchar(18)     NULL,
    office_phone                            varchar(18)     NULL,
    mobile_phone                            varchar(18)     NULL,
    email                                   varchar(100)    NULL,
    comments                                varchar(100)    NULL,
    PRIMARY KEY (client_id)
    );

/*  Table Name:     Tickets
    Description:    Contiene la información de los tikets
*/
create table if not exists "tickets"
(
    ticket_id                               uuid            NOT NULL,
    ticker_code                             varchar(100)    NOT NULL,
    PRIMARY KEY (ticket_id)
    --tientda
    --operador (cajero)
    --transferencia
    --cliente
    --fecha:hora generación
);
