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
using System.Windows.Shapes;
using NapierBank.Models;

namespace NapierBank.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Home();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            btnHome.Background = Brushes.White;
            btnHome.BorderThickness = new Thickness(0);

            btnTweet.Background = Brushes.Aqua;
            btnTweet.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnSMS.Background = Brushes.Aqua;
            btnSMS.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnEmail.Background = Brushes.Aqua;
            btnEmail.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnLists.Background = Brushes.Aqua;
            btnLists.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            Main.Content = new Home();
        }

        private void btnSMS_Click(object sender, RoutedEventArgs e)
        {
            btnSMS.Background = Brushes.White;
            btnSMS.BorderThickness = new Thickness(0);

            btnHome.Background = Brushes.Aqua;
            btnHome.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnTweet.Background = Brushes.Aqua;
            btnTweet.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnEmail.Background = Brushes.Aqua;
            btnEmail.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnLists.Background = Brushes.Aqua;
            btnLists.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            Main.Content = new SMS();
        }

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            btnTweet.Background = Brushes.White;
            btnTweet.BorderThickness = new Thickness(0);

            btnHome.Background = Brushes.Aqua;
            btnHome.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnSMS.Background = Brushes.Aqua;
            btnSMS.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnEmail.Background = Brushes.Aqua;
            btnEmail.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnLists.Background = Brushes.Aqua;
            btnLists.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            Main.Content = new Tweet();
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            btnEmail.Background = Brushes.White;
            btnEmail.BorderThickness = new Thickness(0);

            btnTweet.Background = Brushes.Aqua;
            btnTweet.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnHome.Background = Brushes.Aqua;
            btnHome.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnSMS.Background = Brushes.Aqua;
            btnSMS.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnLists.Background = Brushes.Aqua;
            btnLists.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            Main.Content = new Email();
        }

        private void btnLists_Click(object sender, RoutedEventArgs e)
        {
            btnLists.Background = Brushes.White;
            btnLists.BorderThickness = new Thickness(0);

            btnEmail.Background = Brushes.Aqua;
            btnEmail.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnTweet.Background = Brushes.Aqua;
            btnTweet.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnHome.Background = Brushes.Aqua;
            btnHome.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            btnSMS.Background = Brushes.Aqua;
            btnSMS.BorderThickness = new Thickness(0.5, 0, 0.5, 0.5);

            Main.Content = new Lists();
        }
    }
}
