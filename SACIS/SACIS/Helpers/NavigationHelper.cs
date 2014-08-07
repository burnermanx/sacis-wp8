using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Phone.Controls;

namespace SACIS.Helpers
{
    /// <summary>
    /// Navigation helper, to manage navigation from anywhere
    /// </summary>
    public class NavigationHelper
    {
        private static readonly NavigationHelper _instance = new NavigationHelper();
        public static NavigationHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        private PhoneApplicationFrame _frame;

        private NavigationHelper()
        {

        }

        /// <summary>
        /// Initialize the navigation helper with a navigation frame
        /// </summary>
        /// <param name="frame"></param>
        public void Initialize(PhoneApplicationFrame frame)
        {
            _frame = frame;
        }

        /// <summary>
        /// Go back in navigation history
        /// </summary>
        public void GoBack()
        {
            if (_frame.CanGoBack)
                _frame.GoBack();
        }

        /// <summary>
        /// Navigate to an url
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            _frame.Navigate(new Uri(url, UriKind.Relative));
        }

        /// <summary>
        /// Removes an entry from the navigation back stack
        /// </summary>
        public void RemoveBackEntry()
        {
            _frame.RemoveBackEntry();
        }

        /// <summary>
        /// Exits the application 
        /// </summary>
        public void Exit()
        {
            if (Environment.OSVersion.Version.Major < 8)
            {
                System.Reflection.Assembly asmb = System.Reflection.Assembly.Load("Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
                asmb = System.Reflection.Assembly.Load("Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
                Type type = asmb.GetType("Microsoft.Xna.Framework.Game");
                object obj = type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                type.GetMethod("Exit").Invoke(obj, new object[] { });
            }
            else
            {
                Type type = Application.Current.GetType();
                type.GetMethod("Terminate").Invoke(Application.Current, new object[] { });
            }
        }
    }
}
