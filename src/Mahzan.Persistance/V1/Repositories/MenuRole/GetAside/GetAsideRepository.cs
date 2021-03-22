using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Models.Entities;
using Mahzan.Persistance.V1.Exeptions.MenuRoles.GetAside;
using Mahzan.Persistance.V1.Filters.MenuRole;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.ViewModel.MenuRole;
using Npgsql;
using MenuSection = Mahzan.Persistance.V1.ViewModel.MenuRole.MenuSection;

namespace Mahzan.Persistance.V1.Repositories.MenuRole.GetAside
{
    public class GetAsideRepository:
    BaseFindRepository<GetMenuRoleViewModel,GetMenuRoleFilter>,
    IGetAsideRepository
    {
        public GetAsideRepository(
            NpgsqlConnection connection) 
            : base(connection,model => model.Items.ToString())
        {
        }

        protected override async Task<IReadOnlyList<GetMenuRoleViewModel>> FindInternal(
            GetMenuRoleFilter filter, 
            PagingOptions pagingOptions)
        {

            GetMenuRoleViewModel result = new GetMenuRoleViewModel();
            
            Roles role = await GetUserRole(filter.UserId);

            List<Models.Entities.MenuRole> menu_role = await GetMenuRole(role.RoleId);

            foreach (var item in menu_role)
            {
                //Section
                if (item.MenuSectionId != null
                    && item.MenuSelectionId == null
                    && item.MenuSubMenuId == null)
                {
                    MenuSection section = await GetSection(item);
                    result.Items.Add(section);
                }

                //Selection
                if (item.MenuSectionId !=null 
                && item.MenuSelectionId !=null
                && item.MenuSubMenuId ==null)
                {
                    MenuSection section = await GetSelection(item);
                    result.Items.Add(section);
                }
                
                //Sub Menu
                if (item.MenuSectionId != null
                    && item.MenuSelectionId != null
                    && item.MenuSubMenuId !=null)
                {
                    MenuSection sectionExist = (
                        from ms in result.Items
                        where ms.MenuSelectionId == item.MenuSelectionId
                        select ms
                    ).FirstOrDefault();
                    
                    result.Items
                        .FirstOrDefault(
                            x 
                                => x.MenuSelectionId == item.MenuSelectionId 
                                   && x.MenuSelectionId == item.MenuSelectionId)
                        .Submenu
                        .Add(await GetSubMenu(item.MenuSubMenuId.Value));
                }
            }

            IEnumerable<GetMenuRoleViewModel> enumerable = new List<GetMenuRoleViewModel>()
            {
                result
            };

            return enumerable.ToImmutableList();
        }

        private async Task<Roles> GetUserRole(Guid userId)
        {
            string sql = @"
                select  * 
                from    roles
                inner   join user_role on user_role.role_id = roles.role_id
                where   user_role.user_id = @user_id
            ";

            IEnumerable<Roles> roles = await Connection
                .QueryAsync<Roles>(
                    sql,
                    new
                    {
                        user_id = userId
                    }
                );

            if (!roles.Any())
            {
                throw new GetAsideArgumentException(
                    $"No fue posible encontrar un role asignado al usuario{userId}."
                    );
            }

            return roles.FirstOrDefault();
        }

        private async Task<List<Models.Entities.MenuRole>> GetMenuRole(
            Guid roleId)
        {
            string sql = @"
                select  * 
                from    menu_role
                where   role_id = @role_id
            ";

            IEnumerable<Models.Entities.MenuRole> menuRole;
            menuRole = await Connection
                .QueryAsync<Models.Entities.MenuRole>(
                    sql, 
                    new
                    {
                        role_id = roleId
                    });

            if (!menuRole.Any())
            {
                throw new GetAsideArgumentException(
                    $"No fue posible encontrar el menú para el role {roleId}."
                    );
            }

            return menuRole.ToList();
        }

        private async Task<MenuSection> GetSection(
            Models.Entities.MenuRole menuRole)
        {
            string sql = @"
                select  * 
                from    menu_sections
                where   menu_section_id=@menu_section_id
            ";

            IEnumerable<Models.Entities.MenuSections> menuSection;
            menuSection = await Connection
                .QueryAsync<Models.Entities.MenuSections>(
                    sql,
                    new
                    {
                        menu_section_id = menuRole.MenuSectionId
                    }
                );

            if (!menuSection.Any())
            {
                throw new GetAsideArgumentException(
                    $"No se econtró ninguna sección para el menu.");
            }

            return new MenuSection()
            {
                MenuSectionId = menuSection.FirstOrDefault().MenuSectionId,
                Section = menuSection.FirstOrDefault().Section,
                Root = true
            };
        }

        private async Task<MenuSection> GetSelection(
            Models.Entities.MenuRole menuRole)
        {
            string sql = @"
                select  * 
                from    menu_selections
                where   menu_selection_id=@menu_selection_id
            ";

            IEnumerable<MenuSelections> menuSelection;
            menuSelection = await Connection
                .QueryAsync<MenuSelections>(
                    sql,
                    new
                    {
                        menu_selection_id = menuRole.MenuSelectionId
                    }
                );

            if (!menuSelection.Any())
            {
                throw new GetAsideArgumentException(
                    $"No se econtró ninguna seleccion para la sección del menu.");
            }

            return new MenuSection()
            {
                MenuSelectionId = menuSelection.FirstOrDefault().MenuSelectionId,
                Title = menuSelection.FirstOrDefault().Title,
                Root = menuSelection.FirstOrDefault().Root,
                Bullet = menuSelection.FirstOrDefault().Bullet,
                Icon = menuSelection.FirstOrDefault().Icon
            };
        }

        private async Task<Submenu> GetSubMenu(
            Guid menuSubMenuId)
        {
            string sql = @"
                select * from menu_sub_menu
                where menu_sub_menu_id = @menu_sub_menu_id
            ";

            IEnumerable<Submenu> menuSubMenu;
            menuSubMenu = await Connection
                .QueryAsync<Submenu>(
                    sql,
                    new
                    {
                        menu_sub_menu_id = menuSubMenuId
                    });

            return menuSubMenu.FirstOrDefault();

        }
    }
}