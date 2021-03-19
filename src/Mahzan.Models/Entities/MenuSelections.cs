using System;

namespace Mahzan.Models.Entities
{
    public class MenuSelections
    {
        public Guid MenuSelectionId { get; set; }
        
        public string Title { get; set; }
        
        public bool Root { get; set; }
        
        public string Bullet { get; set; }
        
        public string Icon { get; set; }
        
        public Guid MenuSectionId { get; set; }
    }
}