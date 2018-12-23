using System;
using System.Collections.Generic;
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

namespace NapierBank.Views
{
    /// <summary>
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : Page
    {

        private int listIndex = 0;
        private List<EmailModel> emailList = new List<EmailModel>();

        public Email()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            string header = txtHeader.Text;
            string emailSender = txtSender.Text.Replace(" ", String.Empty);
            string subject = txtSubject.Text;
            string message = txtMessage.Text;
            bool headerOk = Message.HeaderOk(header);
            Regex checkEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex checkSubject = new Regex("^SIR (0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[- /.]()\\d\\d$");
            Match emailMatch = checkEmail.Match(emailSender);
            Match SIRMatch = checkSubject.Match(subject);

            if (!headerOk || !header.ToUpper().StartsWith("E"))
            {
                info.DisplayMessage("Incorrect Header", "Email Header must begin with 'E', followed by 9 numbers");
            }
            else if (!emailMatch.Success)
            {
                info.DisplayMessage("Sender Incorrect", "Please enter a valid email address in the sender box.");
            }
            else if (message == "")
            {
                info.DisplayMessage("No Message", "Please enter a message");
            }
            else
            {
                string[] messageArray = message.Split(' ');
                string[] removedURL = EmailModel.RemoveURL(messageArray);
                txtMessage.Text = string.Join(" ", removedURL);
                EmailModel email = new EmailModel(header, txtMessage.Text, emailSender, subject);

                if (subject.ToUpper().StartsWith("SIR"))
                {
                    if (SIRMatch.Success)
                    {
                        if (email.HandleSIR())
                        {
                            info.DisplayMessage("Success", "Significant Incident Report submitted");
                        }
                        else
                        {
                            info.DisplayMessage("Error", "It seems you tried to submit a significant incident report but it is not laid out correctly. " +
                                "You must have a sort code in the form of 99-99-99 on the first line, and the incident on the second");
                        }
                    } else
                    {
                        info.DisplayMessage("Oops!", "It looks like you tried to submit an SIR but the subject isn't formatted correctly. It must be SIR dd/mm/yy.");
                    }
                } else
                {
                    email.OutputJson();
                    info.DisplayMessage("Success", "Email submitted successfuly.");
                }

            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtHeader.Clear();
            txtSubject.Clear();
            txtSender.Clear();
            txtMessage.Clear();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            emailList = EmailModel.ReadJson();
            if(emailList.Count == 0)
            {
                Info info = new Info("No messages", "No emails to load");
                info.DisplayMessage();
            } else
            {
                emailList.Remove(emailList.Last());
                listIndex = emailList.IndexOf(emailList.Last());
                LoadTextBoxes();
            }
        }

        private void LoadTextBoxes()
        {
            txtHeader.Text = emailList[listIndex].Header;
            txtSender.Text = emailList[listIndex].Sender;
            txtMessage.Text = emailList[listIndex].Body;

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
            if (listIndex < emailList.Count - 1)
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
