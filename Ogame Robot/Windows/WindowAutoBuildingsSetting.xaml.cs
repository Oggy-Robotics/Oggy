using Ogame_Robot.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ogame_Robot
{
    /// <summary>
    /// Interakční logika pro WindowAutoBuildingsSetting.xaml
    /// </summary>
    public partial class WindowAutoBuildingsSetting : Window
    {
        public WindowAutoBuildingsSetting()
        {
            InitializeComponent();
            DataBase.dataBase.windowCollection.Add(this);
            Closing += this.OnWindowClosing;
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < DataBase.dataBase.windowCollection.Count(); i++)
            {
                if (DataBase.dataBase.windowCollection[i] is WindowAutoBuildingsSetting)
                {
                    DataBase.dataBase.windowCollection.RemoveAt(i);
                }
            }
        }
    }
}
