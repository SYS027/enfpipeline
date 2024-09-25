﻿using EDF.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EDF.Web.Models
{
    public class EmailHelper
    {

        static string email_template_path = ConfigurationManager.AppSettings["email_template_path"];
        static string PrimaryMailServer = ConfigurationManager.AppSettings["SmtpPrimaryMailServer"];

        static string SENDER_EMAIL = ConfigurationManager.AppSettings["SenderEmail"];
        static string SENDER_PASS = ConfigurationManager.AppSettings["SenderPass"];
        static int PORT = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        static bool IsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSsl"]);
        public static string GetEmailBody(string template_name)
        {
            string emailBody = string.Empty;

            if (File.Exists(email_template_path + "\\" + template_name))
            {
                using (StreamReader reader = new StreamReader(email_template_path + "\\" + template_name))
                {
                    emailBody = reader.ReadToEnd();
                    //Helper.WriteLog("email body :" + emailBody);
                }
            }
            return emailBody;
        }
        public static bool SendEmail(EmailModel email_model, string AlertCategory)
        {
            try
            {

                SmtpClient smtpClient;
                smtpClient = new SmtpClient(PrimaryMailServer, PORT);
                using (MailMessage message = new MailMessage())
                {
                    message.IsBodyHtml = true;
                    MailAddress mailAddress = new MailAddress(SENDER_EMAIL);
                    message.Sender = mailAddress;
                    message.From = mailAddress;

                    switch (AlertCategory)
                    {
                        case "CONFIRM":
                            {
                                message.Body = email_model.email_body;
                                message.Subject = email_model.email_subject;

                                for (int i = 0; i < email_model.to_emailids.Count; i++)
                                {
                                    message.To.Add(email_model.to_emailids[i]);
                                }
                                if (email_model.cc_emailids != null)
                                {
                                    for (int i = 0; i < email_model.cc_emailids.Count; i++)
                                    {
                                        message.CC.Add(email_model.cc_emailids[i]);
                                    }
                                }
                                if (email_model.bcc_emailids != null)
                                {
                                    for (int i = 0; i < email_model.bcc_emailids.Count; i++)
                                    {
                                        message.Bcc.Add(email_model.bcc_emailids[i]);
                                    }
                                }
                                if (email_model.eml_attachemet != null)
                                {
                                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(new MemoryStream(email_model.eml_attachemet), email_model.attachment_name, "application/pdf");
                                    message.Attachments.Add(attach);
                                    //for (int k = 0; k < email_model.attached_files.Count; k++)
                                    //{
                                    //    if (File.Exists(AttachmentDir + "\\" + email_model.attached_files[k]))
                                    //    {
                                    //        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(AttachmentDir + "\\" + email_model.attached_files[k]);
                                    //        message.Attachments.Add(attach);
                                    //    }
                                    //}
                                }
                                break;
                            }
                    }
                    smtpClient.Send(message);
                    smtpClient.Dispose();
                    return true;
                }

            }
            catch (Exception ex)
            {
                CommonHelper.write_log("error while sending emails :" + ex.Message);
                return false;
            }
        }
    }
}