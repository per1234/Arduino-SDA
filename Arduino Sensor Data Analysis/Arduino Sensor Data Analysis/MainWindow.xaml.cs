﻿using System;
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
using System.Data;
using System.Threading;

namespace Arduino_Sensor_Data_Analysis
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SDA_Program.Interface.MainWindowInterface IO;
        SDA_Program.View.Home HomePage;
        SDA_Program.View.Statistics StatisticsPage;
        SDA_Program.View.Configurations ConfigurationsPage;

        public MainWindow()
        {
            InitializeComponent();

            IO = new SDA_Program.Interface.MainWindowInterface();

            IO.HomeClicked(B_Home, B_Statistics, B_Configurations, F_Frame);

        }

        private void B_Home_Click(object sender, RoutedEventArgs e)
        {
            IO.HomeClicked(B_Home, B_Statistics, B_Configurations, F_Frame);
        }

        private void B_Configurations_Click(object sender, RoutedEventArgs e)
        {
            IO.ConfigurationClicked(B_Home, B_Statistics, B_Configurations, F_Frame);
        }

        private void B_Statistics_Click(object sender, RoutedEventArgs e)
        {
            IO.StatisticsClicked(B_Home, B_Statistics, B_Configurations, F_Frame);
        }
    }
}
