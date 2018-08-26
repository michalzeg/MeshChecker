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

namespace IncoherentMeshChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            foreach (string s in listBoxResults.ItemsSource)
            {
                text.Append(s);
               text.Append(" ");
            }

            //string text = string.Join("\r\n", this.listBoxResults.ItemsSource);
            //string text = string.Join(" ", listBoxResults.ItemsSource);
            Clipboard.SetText(text.ToString());
            //Clipboard
        }
    }
}