using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mensarium.Components
{
    public static class MSettings
    {
        public static Session CurrentSession { get; set; }
        public static Server Server { get; set; }
        public static Mensa CurrentMensa { get; set; }


        public static void LoadSettings()
        {
            MSettings.CurrentMensa = Mensa.Mensas[0];
        }
        public static void SaveSettings() { }
    }
}