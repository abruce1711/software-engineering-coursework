using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank.Models
{
    /*
        Class for displaying messages to the user
    */
    class Info
    {
        public string Title { get; set; }
        public string Body { get; set; }

        // Constructor
        public Info()
        {
            Title = "";
            Body = "";
        }

        // Constructor
        public Info(string title, string body)
        {
            Title = title;
            Body = body;
        }

        // Displays message without parameters
        public void DisplayMessage()
        {
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(Body, Title, buttons, icon);
        }

        // Displays message with parameters
        public void DisplayMessage(string title, string body)
        {
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(body, title, buttons, icon);
        }
    }
}
