using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBank.Models
{
    /*
       Email subclass.
       This is a subclass which handles email only operations.
    */
    class EmailModel : Message
    {
        public String Subject { get; set; }

        // Constructor
        public EmailModel()
        {
            Header = "";
            Body = "";
            Sender = "";
            Subject = "";
        }

        // Constructor
        public EmailModel(string header, string body, string sender, string subject)
        {
            Header = header;
            Body = body;
            Sender = sender;
            Subject = subject;
        }

        // Picks up URL in text, removes it and stores it in a CSV file for later display.
        public static string[] RemoveURL(string[] messageArray)
        {
            int index = 0;
            foreach(string word in messageArray)
            {
                if(word.ToLower().StartsWith("http") || word.ToLower().StartsWith("www"))
                {
                    messageArray[index] = "<URL Quarantined>";
                    DirectoryInfo di = Directory.CreateDirectory("lists");
                    string file = "lists\\quarantined_urls.csv";
                    if (!File.Exists(file))
                    {
                        File.WriteAllText(file, "Quarantined URL's\n");
                    }

                    File.AppendAllText(file, word + '\n');
                }
                index++;
            }
            return messageArray;
        }

        // Parses the incident list and returns as List for later use
        public static List<string> ParseIncidentList()
        {

            using(StreamReader reader = new StreamReader("_read\\IncidentList.csv"))
            {
                List<string> IncidentList = new List<string>();
                while(!reader.EndOfStream)
                {
                    IncidentList.Add(reader.ReadLine().ToLower());
                }
                return IncidentList;
            }
        }

        // Parses URL list for later use
        public static List<string> ReadURLList()
        {
            List<string> urlList = new List<string>();
            using (var reader = new StreamReader("lists\\quarantined_urls.csv"))
            {
                while (!reader.EndOfStream)
                {
                    urlList.Add(reader.ReadLine().ToLower());
                }
            }
            return urlList;
        }

        // Handles a Significant Incident Report, checking the sort code, incident, and storing it in appropriate files.
        public bool HandleSIR()
        {
            bool SIR = false;
            List<string> IncidentList = ParseIncidentList();
            List<string> lineList = new List<string>();
            Regex checkSortCode = new Regex(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b");
            string sortCode = "";
            string incident = "";

            using (StringReader reader = new StringReader(this.Body))
            {
                string line = "";
                do
                {
                    line = reader.ReadLine();
                    if(line != null)
                    {
                        lineList.Add(line);
                    }
                } while (line != null);
            }

            if(lineList.Count > 1)
            {
                sortCode = lineList[0];
                incident = lineList[1];
            } else
            {
                return false;
            }

            Match matchSortCode = checkSortCode.Match(sortCode);
            if (matchSortCode.Success && IncidentList.Contains(incident.ToLower()))
            {
                DirectoryInfo di = Directory.CreateDirectory("lists");
                string file = "lists\\incident_reports.csv";
                if (!File.Exists(file))
                {
                    File.WriteAllText(file, "Sort Code, Incident\n");
                }

                File.AppendAllText(file, sortCode + "," + incident + '\n');

                SIR sir = new SIR(this.Header, this.Body, this.Sender, this.Subject, incident, sortCode);
                sir.OutputJson();

                SIR = true;
            }
            return SIR;
        }

        // Read from the email JSON file, deserializes it and returns list.
        public static List<EmailModel> ReadJson()
        {
            DirectoryInfo di = Directory.CreateDirectory("emails");
            List<EmailModel> processedEmailList = new List<EmailModel>();
            if(File.Exists("emails\\emails.json"))
            {
                string file = File.ReadAllText("emails\\emails.json");
                string[] jsonList = file.Split('-');

                foreach (string item in jsonList)
                {
                    EmailModel emails = JsonConvert.DeserializeObject<EmailModel>(item);
                    processedEmailList.Add(emails);
                }
            }
            return processedEmailList;
        }

    }
}