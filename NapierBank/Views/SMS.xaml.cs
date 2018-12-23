using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;

namespace NapierBank.Views
{
    /// <summary>
    /// Interaction logic for SMS.xaml
    /// </summary>
    public partial class SMS : Page
    {
        int listIndex = 0;
        List<SMSModel> smsList = new List<SMSModel>();

        public SMS()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            string header = txtHeader.Text;
            string smsSender = txtSender.Text.Replace(" ", String.Empty);
            string message = txtMessage.Text;
            bool headerOk = Message.HeaderOk(header);
            Regex checkPhone = new Regex("\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14}$");
            Match phoneMatch = checkPhone.Match(smsSender);
            bool pm = phoneMatch.Success;

            if (!headerOk || !header.ToUpper().StartsWith("S"))
            {
                info.DisplayMessage("Incorrect Header", "SMS Header must begin with 'S', followed by 9 numbers");
            } else if(!phoneMatch.Success)
            {
                info.DisplayMessage("Sender Incorrect", "Please enter a phone number, including the area code in the sender box.");
            } else if (message == "")
            {
                info.DisplayMessage("No Message", "Please enter a message");
            }
            else
            {
                txtMessage.Text = string.Join(" ", TSA.ReplaceWords(txtMessage.Text.Split(' ')));
                SMSModel sms = new SMSModel(header, txtMessage.Text, smsSender);
                sms.OutputJson();
                info.DisplayMessage("Success", "SMS submitted successfuly");
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
            smsList = SMSModel.ReadJson();
            if(smsList.Count == 0)
            {
                Info info = new Info("No messages", "No messages to load");
                info.DisplayMessage();
            } else
            {
                smsList.Remove(smsList.Last());
                listIndex = smsList.IndexOf(smsList.Last());
                LoadTextBoxes();
            }
        }

        private void LoadTextBoxes()
        {
            txtHeader.Text = smsList[listIndex].Header;
            txtSender.Text = smsList[listIndex].Sender;
            txtMessage.Text = smsList[listIndex].Body;

            btnPrev.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible;
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if(listIndex > 0)
            {
                listIndex--;
                LoadTextBoxes();
            } else
            {
                Info info = new Info("Out of messages", "No more messages");
                info.DisplayMessage();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if(listIndex < smsList.Count-1)
            {
                listIndex++;
                LoadTextBoxes();
            } else
            {
                Info info = new Info("Out of messages", "No more messages");
                info.DisplayMessage();
            }
        }
    }
}
