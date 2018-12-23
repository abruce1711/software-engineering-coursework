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
using NapierBank.Models;

namespace NapierBank.Views
{
    /// <summary>
    /// Interaction logic for Tweet.xaml
    /// </summary>
    public partial class Tweet : Page
    {
        int listIndex = 0;
        List<TweetModel> tweetList = new List<TweetModel>();

        public Tweet()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            string header = txtHeader.Text;
            string tweetSender = txtSender.Text.Replace(" ", String.Empty);
            string message = txtMessage.Text;
            bool headerOk = Message.HeaderOk(header);

            if (!headerOk || !header.ToUpper().StartsWith("T"))
            {
                info.DisplayMessage("Incorrect Header", "SMS Header must begin with 'T', followed by 9 numbers");
            }
            else if (!tweetSender.StartsWith("@"))
            {
                info.DisplayMessage("Sender Incorrect", "Sender must be a valid Twitter handle beginning with @");
            } else if (message == "")
            {
                info.DisplayMessage("No Message", "Please enter a message");
            }
            else
            {
                txtMessage.Text = string.Join(" ", TSA.ReplaceWords(txtMessage.Text.Split(' ')));
                TweetModel tweet = new TweetModel(header, message, tweetSender);
                tweet.ParseTweet();
                tweet.OutputJson();
                info.DisplayMessage("Success", "Tweet submitted successfuly");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtHeader.Clear();
            txtSender.Clear();
            txtMessage.Clear();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            tweetList = TweetModel.ReadJson();
            if(tweetList.Count == 0)
            {
                Info info = new Info("No messages", "No Tweets to load");
                info.DisplayMessage();
            } else
            {
                tweetList.Remove(tweetList.Last());
                listIndex = tweetList.IndexOf(tweetList.Last());
                LoadTextBoxes();
            }
        }

        private void LoadTextBoxes()
        {
            txtHeader.Text = tweetList[listIndex].Header;
            txtSender.Text = tweetList[listIndex].Sender;
            txtMessage.Text = tweetList[listIndex].Body;

            btnPrev.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible;
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (listIndex > 0)
            {
                listIndex--;
                LoadTextBoxes();
            }
            else
            {
                Info info = new Info("Out of messages", "No more messages");
                info.DisplayMessage();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (listIndex < tweetList.Count - 1)
            {
                listIndex++;
                LoadTextBoxes();
            }
            else
            {
                Info info = new Info("Out of messages", "No more messages");
                info.DisplayMessage();
            }
        }
    }
}
