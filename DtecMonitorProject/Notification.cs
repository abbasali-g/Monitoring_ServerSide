using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DtecMonitorProject
{
    public class Notification
    {
        public async Task sendSmsAsyn(string smsReceivers,string smsText)
        {
            try
            {
                smsText = smsText + "\n" + "لغو:۱۱";  
                string[] receivers = smsReceivers.Split(',',StringSplitOptions.RemoveEmptyEntries);
                if(receivers.Length==0)
                    return;
                
                string smsUrl = "http://webservice.iran.tc/URL/";
                HttpClient client = new HttpClient();
                foreach (var rcv in receivers)
                {
                    IList<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>
                    {
                        {new KeyValuePair<string, string>("uid", "abbasali_g")},
                        {new KeyValuePair<string, string>("pass", "135406001NwDIC")},
                        {new KeyValuePair<string, string>("tel", rcv)},
                        {new KeyValuePair<string, string>("body", smsText)}

                    };
                    var a = client.PostAsync(smsUrl, new FormUrlEncodedContent(nameValueCollection)).Result;
                }
                
            }
            catch (Exception e)
            {
                await Utility.writeErrorAsync("sendSmsAsyn:"+e.Message);
            }

        }

        public  async Task sendEmailAsync(string emailReceivers,string subject,string body)
        {
            try
            {
                string[] emails = emailReceivers.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                if (emailReceivers=="" || emails.Length == 0)
                    return;
                
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl=true;
                
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("dadehtamin@gmail.com", "DDtec1690");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("dadehtamin@gmail.com");
                foreach (var address in emailReceivers.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries))
                    mailMessage.To.Add(address);    
                
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                await client.SendMailAsync(mailMessage);
                //await client.SendMailAsync("dadehtamin@gmail.com","a.gharakhanlou@gmail.com","Test","body");
            }
            catch (Exception e)
            {
                await Utility.writeErrorAsync("sendEmailAsync:"+e.Message);
            }
            
        }
    }
}