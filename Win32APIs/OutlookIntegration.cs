using System;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Win32APIs
{
    public class OutlookIntegration
    {
        public static void SendOutlookEmail(string to, string subject, string body, Outlook.OlImportance importance, bool display)
        {
          try
          { 
                Outlook.Application OutlookApp = new Outlook.Application();
                Outlook.MailItem MailItem = (Outlook.MailItem)OutlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                MailItem.To = to;
                MailItem.Subject = subject; 
                MailItem.Body = body;
                MailItem.Importance = importance;

                if(display) 
                    MailItem.Display(true);
                else
                    MailItem.Send();
          }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}
