using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NapierBank.Models
{
    /* 
       The parent Message class.
       This class is the parent class for each of the message types.
    */
    class Message
    {
        public string Header { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }

        // Outputs each message to the appropriate JSON file depending on the header
        public void OutputJson()
        {
            string file = "";
            
            if(this.Header.StartsWith("S"))
            {
                DirectoryInfo di = Directory.CreateDirectory("sms");
                file = "sms\\sms.json";
            } else if (this.Header.StartsWith("T"))
            {
                DirectoryInfo di = Directory.CreateDirectory("tweets");
                file = "tweets\\tweets.json";
            } else if (this.Header.StartsWith("E"))
            {
                DirectoryInfo di = Directory.CreateDirectory("emails");
                file = "emails\\emails.json";
            }

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            if (!File.Exists(file))
            {
                File.WriteAllText(file, json + '-');
            }
            else
            {
                File.AppendAllText(file, json + '-');
            }
        }

        // Checks the header follows the correct format
        public static bool HeaderOk(string header)
        {
            bool headerOk = true;
            if(header.Length != 10)
            {
                headerOk = false;
            }

            foreach (char letter in header.Skip(1))
            {
                if (!char.IsDigit(letter))
                {
                    headerOk = false;
                }
            }

            return headerOk;
        }
    }
}
