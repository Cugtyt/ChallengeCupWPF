using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChallengeCupV1.View
{
    public static class UserControlManager
    {
        private static List<UserControlElement> UserControls = new List<UserControlElement>();
        
        public static void Register(UserControl userControl, string name)
        {
            UserControls.Add(new UserControlElement() { Name = name, UserControlRefer = userControl });
        }

        public static UserControl Get(string name)
        {
            return (from uc in UserControls where uc.Name == name select uc).First().UserControlRefer;
        }

        class UserControlElement
        {
            public string Name;
            public UserControl UserControlRefer;
        } 
    }
}
