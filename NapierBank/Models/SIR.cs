using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBank.Models
{
    /*
        Significant incident report subclass.
        This is a subclass which handles SIR only operations.
    */
    class SIR : EmailModel
    {
        public string Incident { get; set; }
        public string SortCode { get; set; }

        // Constructor
        public SIR()
        {
            Header = "";
            Body = "";
            Incident = "";
            SortCode = "";
        }

        // Constructor
        public SIR(string header, string body, string sender, string subject, string incident, string sortCode)
        {
            Header = header;
            Body = body;
            Sender = sender;
            Subject = subject;
            Incident = incident;
            SortCode = sortCode;
        }
    }
}
