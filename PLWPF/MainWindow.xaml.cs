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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            bl = Factory_BL.getBL();

        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            TesterWindow testerWindow = new TesterWindow();
            testerWindow.ShowDialog();

        }

        private void TrineeButton_Click(object sender, RoutedEventArgs e)
        {
            TrinneWindow trinneWindow = new TrinneWindow();
            trinneWindow.ShowDialog();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            TestMenuxaml testMenu = new TestMenuxaml();
            testMenu.ShowDialog();
        }
    }
}
