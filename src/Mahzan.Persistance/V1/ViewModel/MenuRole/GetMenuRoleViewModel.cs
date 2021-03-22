using System;
using System.Collections.Generic;

namespace Mahzan.Persistance.V1.ViewModel.MenuRole
{
    public class GetMenuRoleViewModel
    {
        public List<MenuSection> Items = new List<MenuSection>();
    }

    public class MenuSection
    {
        public Guid? MenuSectionId { get; set; }

        public Guid? MenuSelectionId { get; set; }
        
        public string Section { get; set; }
        
        public string Title { get; set; }
        
        public bool Root { get; set; }
        
        public string Bullet { get; set; }
        
        public string Icon { get; set; }

        public List<Submenu> Submenu = new List<Submenu>();
    }

    public class Submenu
    {
        public Guid MenuSubMenuId { get; set; }

        public string Title { get; set; }
        
        public string Bullet { get; set; }
        
        public string Page { get; set; }
    }
}