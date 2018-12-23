using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBank.Models
{
    /*
       SMS subclass.
       This is a subclass which handles SMS only operations.
    */  
    class SMSModel : Message
    {
        
        // Constructor
        public SMSModel()
        {
            Header = "";
            Body = "";
            Sender = "";
        }

        // Constructor
        public SMSModel(string header, string body, string phone)
        {
            Header = header;
            Body = body;
            Sender = phone;
        }

        // Reads from the SMS JSON file, deserializes it and returns list.
        public static List<SMSModel> ReadJson()
        {
            DirectoryInfo di = Directory.CreateDirectory("sms");
            List<SMSModel> processedSMSList = new List<SMSModel>();
            if(File.Exists("sms\\sms.json"))
            {
                string file = File.ReadAllText("sms\\sms.json");
                string[] jsonList = file.Split('-');

                foreach (string item in jsonList)
                {
                    SMSModel sms = JsonConvert.DeserializeObject<SMSModel>(item);
                    processedSMSList.Add(sms);
                }
            }
            return processedSMSList;
        }
    }
}
