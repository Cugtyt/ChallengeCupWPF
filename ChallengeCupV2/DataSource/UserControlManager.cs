using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChallengeCupV2.DataSource
{
    /// <summary>
    /// User controls in view folder for calling user control class methods easily
    /// </summary>
    public static class UserControlManager
    {
        private static List<UserControlElement> UserControls = new List<UserControlElement>();
        
        public static void Register(UserControl userControl, string name)
        {
            UserControls.Add(new UserControlElement() { Name = name, UserControlRefer = userControl });
        }

        /// <summary>
        /// Get user control by name
        /// because user control name is unique, return first element of result set is ok
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
