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
using PublicLibrary.lip;

namespace PublicLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePublisher.xaml
    /// </summary>
    public partial class PagePublisher : Page
    {
        DbContext db = new DbContext(MainWindow.path);

        public PagePublisher()
        {
            InitializeComponent();
           
            gvPublisher.DataContext = new Publisher();
            lwPublishers.ItemsSource = db.GetAllPubs();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Publisher pub = (Publisher)gvPublisher.DataContext;

            if (pub.Id != 0)
            {
                db.EditPublisher(pub);
            }
            else
            {
                db.AddPublisher(pub);
            }
            lwPublishers.ItemsSource = db.GetAllPubs();
        }

        // событие по клику на итем
        //private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    gvPublisher.DataContext = (Publisher)((ListViewItem)sender.);
        //}

        private void lwPublishers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gvPublisher.DataContext = (Publisher)(((ListView)sender).SelectedItem);
        }
    }
}
