using API.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.CommonMethods
{
    public class Common
    {
        public static string Encrypt(string input)
        {
            string Pass = "";
            try
            {
                string key = "sblw-3hn8-sqoy19";
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                Pass = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex) { Pass = input; }
            return Pass;
        }
        public static string Decrypt(string input)
        {
            string Pass = "";
            try
            {
                string key = "sblw-3hn8-sqoy19";
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                Pass = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex) { Pass = input; }
            return Pass;
        }

        public static UserModel DecodeToken(string Token)
        {
            var jwt = new JwtSecurityTokenHandler();
            string token = Token.Replace("Bearer ", "");
            var read = jwt.ReadToken(token) as JwtSecurityToken;
            var ssl = read.Claims.FirstOrDefault(row => row.Type == "unique_name").Value;
            UserModel model = new UserModel()
            {
                ClientID = Convert.ToInt32(read.Claims.FirstOrDefault(row => row.Type == "nameid").Value),
                UserID = Convert.ToInt32(read.Claims.FirstOrDefault(row => row.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value),
                RoleID = Convert.ToInt32(read.Claims.FirstOrDefault(row => row.Type == "role").Value),
                Email = read.Claims.FirstOrDefault(row => row.Type == "email").Value,
                //mobileNo = read.Claims.FirstOrDefault(row=>row.Type== "MobilePhone").Value
            };
            return model;
        }
        public static EndUserModel EndUserDecodeToken(string Token)
        {
            var jwt = new JwtSecurityTokenHandler();
            string token = Token.Replace("Bearer ", "");
            var read = jwt.ReadToken(token) as JwtSecurityToken;
            var ssl = read.Claims.FirstOrDefault(row => row.Type == "unique_name").Value;
            EndUserModel model = new EndUserModel()
            {
                ClientID = Convert.ToInt32(read.Claims.FirstOrDefault(row => row.Type == "nameid").Value),
                UserID = Convert.ToInt32(read.Claims.FirstOrDefault(row => row.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value),
                Email = read.Claims.FirstOrDefault(row => row.Type == "email").Value,
            };
            return model;
        }
        public static string mailMaster(string fromEmail, string toEmail, string subject, string msgBody, 
            string _Userid, string _Password,string Host,int port,bool UseDefaultCredentials,bool Ssl ,ref string Message)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage mail = new MailMessage();
                /*
                By: Sunil
                Use: implement ";" separated multimple emails to emailTo
                Date: 2021/08/05
                 */
                if (toEmail.Contains(";"))
                {
                    foreach (var item in toEmail.Split(';'))
                    {
                        mail.To.Add(item);
                    }
                }
                else
                {
                    mail.To.Add(toEmail);
                }
                //mail.To.Add(toEmail);
                mail.From = new MailAddress(fromEmail);
                mail.Subject = subject;
                mail.Body = msgBody;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = port;
                smtp.UseDefaultCredentials = UseDefaultCredentials;
                smtp.Credentials = new System.Net.NetworkCredential(_Userid, _Password);
                smtp.EnableSsl = Ssl;
                //await smtp.SendMailAsync(mail);
                smtp.Send(mail);
                Message= "Success";
            }
            catch (Exception ex)
            {
                Message = "Exception : " + ex.Message;
            }
            return Message;
        }

        public static string MailMasterWithAttachment(string fromEmail, string toEmail, string subject, string msgBody,
          string _Userid, string _Password, string Host, int port, bool UseDefaultCredentials, bool Ssl, Attachment attachment)
        {
            string Message = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage mail = new MailMessage();

                if (toEmail.Contains(";"))
                {
                    foreach (var item in toEmail.Split(';'))
                    {
                        mail.To.Add(item);
                    }
                }
                else
                {
                    mail.To.Add(toEmail);
                }
                mail.Attachments.Add(attachment);
                mail.From = new MailAddress(fromEmail);
                mail.Subject = subject;
                mail.Body = msgBody;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = port;
                smtp.UseDefaultCredentials = UseDefaultCredentials;
                smtp.Credentials = new System.Net.NetworkCredential(_Userid, _Password);
                smtp.EnableSsl = Ssl;
                smtp.Send(mail);
                Message = "Success";
            }
            catch (Exception ex)
            {
                Message = "Exception : " + ex.Message;
            }
            return Message;
        }

        public static bool SendSms(string url,out string Response)
        {

            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            string status = string.Empty;
            try
            {
                request = WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Response = result;
                reader.Close();
                stream.Close();
                return true;
            }
            catch (Exception exp) { Response = exp.Message; return false; }
            finally { if (response != null) response.Close(); }
        }
    }
}
