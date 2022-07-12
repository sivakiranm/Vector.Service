using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.UserManagement.Entities
{
    class UserFeatures
    {
    }

    public class UserFeatureAccess
    {
        public List<MainMenu> MenuItems { get; set; }
    }

    public class MainMenu
    {
        public string MainMenuType { get; set; }
        public string MainMenuName { get; set; }
        public int MainMenuId { get; set; }
        public string MainMenuRoutePath { get; set; }
        public List<Menu> Menu { get; set; }
    }

    public class Menu
    {
        public string MenuType { get; set; }
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuRoutePath { get; set; }
        public List<SubMenu> SubMenu { get; set; }
    }

    public class SubMenu
    {
        public string SubMenuType { get; set; }
        public string SubMenuName { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuRoutePath { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public string FeatureType { get; set; }
        public string FeatureName { get; set; }
        public int FeatureId { get; set; }
        public string FeatureRoutePath { get; set; }
        public bool IsPinned { get; set; }
        public string Icon { get; set; }
        public string FeatureDescription { get; set; }
        public string FeatureClass { get; set; }
    }
}
