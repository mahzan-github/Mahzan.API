using System;

namespace Mahzan.Models.Entities
{
    public class MenuSubMenu
    {
        public Guid MenuSubMenuId { get; set; }
        
        public string Title { get; set; }
        
        public string Page { get; set; }
        
        public string Bullet { get; set; }
        
        public Guid MenuSelectionId { get; set; }
    }
}