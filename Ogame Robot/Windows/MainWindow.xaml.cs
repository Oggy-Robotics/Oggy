using Ogame_Robot.Clases;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ogame_Robot
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataBase.Inicialize();
            FilesOperations.InicializationFileLoad();


            InitializeComponent();
            if (DataBase.dataBase.inicializationFile.autologin)
            {
                NewBrowserThreat.Start();
            }

            PassInicializationText();
            DataBase.dataBase.windowCollection.Add(this);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonStartBrowser(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text.Length > 3 && TextBoxUsername.Text.Length > 3)
            {
                DataBase.dataBase.inicializationFile.password = TextBoxPassword.Text;
                DataBase.dataBase.inicializationFile.username = TextBoxUsername.Text;
                DataBase.dataBase.inicializationFile.autologin = (bool)Autologin.IsChecked;

                FilesOperations.InicializationFileSave();
                if (DataBase.dataBase.thread == null)
                    NewBrowserThreat.Start();
            }
        }
        private void PassInicializationText()
        {
            TextBoxPassword.Text = DataBase.dataBase.inicializationFile.password;
            TextBoxUsername.Text = DataBase.dataBase.inicializationFile.username;
            Autologin.IsChecked = DataBase.dataBase.inicializationFile.autologin;
        }
        private void ButtonShowWindowAutoBuildingsSetting(object sender, RoutedEventArgs e)
        {
            bool contained = false;
            foreach (Window item in DataBase.dataBase.windowCollection)
            {
                if (item is WindowAutoBuildingsSetting)
                {
                    contained = true;
                }
            }
            if (!contained)
            {
                WindowAutoBuildingsSetting win2 = new WindowAutoBuildingsSetting();
                win2.Show();
            }

        }

    }
}
