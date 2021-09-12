using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class BusinessReport
    {
    }
    public class GetPDF
    {
        public string value { get; set; }
        public string getpolicyby { get; set; }
        public string policytype { get; set; }
        public string product { get; set; }
        public string insurer { get; set; }
        public string path { get; set; }
        public string clienturl { get; set; }
        public int ClientID { get; set; }
        public string vehicleno { get; set; }
        public string Enquiryno { get; set; }
        public string policyno { get; set; }
        public string proposalno { get; set; }
        public string agentcode { get; set; }
        public string corelationid { get; set; }
        public string productcode { get; set; }
    }
    public class GotoProposalRespose
    {
        public string Status { get; set; }
        public string PolicyType { get; set; }
        public string EncryptENQ { get; set; }
    }
    public class ReturnForPDF
    {
        public string Clienturl { get; set; }
        public string Policyno { get; set; }
    }
}
