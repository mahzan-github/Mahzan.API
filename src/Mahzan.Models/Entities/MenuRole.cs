using System;

namespace Mahzan.Models.Entities
{
    public class MenuRole
    {
        public Guid MenuRoleId { get; set; }
        
        public Guid? MenuSectionId { get; set; }
        
        public Guid? MenuSelectionId { get; set; }
        
        public Guid? MenuSubMenuId { get; set; }
        
        public Guid? MenuSubMenuItemId { get; set; }
        
        public int  Order { get; set; }
        
        public Guid RoleId { get; set; }
    }
}