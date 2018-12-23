using NapierBank.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NapierBank.Views
{
    /// <summary>
    /// Interaction logic for Lists.xaml
    /// </summary>
    public partial class Lists : Page
    {
        public Lists()
        {
            InitializeComponent();
            RefreshLists();
        }

        public void DisplayTrendingList()
        {
            if(File.Exists("lists\\hashtags.csv"))
            {
                List<string> trendingList = TweetModel.ProduceTrendingList();
                foreach (string item in trendingList)
                {
                    lstTrending.Items.Add(item);
                }
            }
        }

        public void DisplayUrlList()
        {
            if(File.Exists("lists\\quarantined_urls.csv"))
            {
                List<string> URLList = EmailModel.ReadURLList();
                URLList.Remove("quarantined url's");
                foreach (string item in URLList)
                {
                    lstURL.Items.Add(item);
                }
            }
        }

        public void DisplayMentions()
        {
            if(File.Exists("lists\\mentions.csv"))
            {
                List<string> mentions = TweetModel.ReadMentions();
                mentions.Remove("mentions");
                foreach (string item in mentions)
                {
                    lstMentions.Items.Add(item);
                }
            }
        }

        public void DisplaySIR()
        {
            if(File.Exists("lists\\incident_reports.csv"))
            {
                List<string> incidents = EmailModel.ReadIncidentList();
                incidents.Remove("sort code, incident");
                foreach (string item in incidents)
                {
                    string[] items = item.Split(',');
                    string separatedItem = items[0] + " - " + items[1];
                    lstIncidents.Items.Add(separatedItem);
                }
            }
        }

        public void RefreshLists()
        {
            DisplayMentions();
            DisplayTrendingList();
            DisplayUrlList();
            DisplaySIR();
        }
    }
}
