using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeetingScheduler.Entities;

namespace MeetingScheduler.Common
{
    public static class Helper
    {
        public static bool SendEmail(string to, string subject, string content)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                StringBuilder strBody = new StringBuilder();
                message.Subject = subject;
                if (subject != null && subject == "Forget Password")
                {
                    message.Subject = "Follow the instructions to reset your password!!";
                }
                message.From = new MailAddress("dotnetfortegroup11@gmail.com");
                message.To.Add(new MailAddress(to));
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dotnetfortegroup11@gmail.com", "kyet xiue pqla vaxo");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return false;
        }


        public static bool SendMeetingEmail(List<string> to, MeetingDTO meetingDTO)
        {
            try
            {
                MailMessage message = new MailMessage();
                foreach (string item in to)
                {
                    message.To.Add(new MailAddress(item));

                }
                SmtpClient smtp = new SmtpClient();
                StringBuilder strBody = new StringBuilder();
                message.Subject = meetingDTO.Subject;
                message.From = new MailAddress("dotnetfortegroup11@gmail.com");
                message.IsBodyHtml = true;
                message.Body = GetEmailContent(meetingDTO.Subject, meetingDTO);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dotnetfortegroup11@gmail.com", "kyet xiue pqla vaxo");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return false;
        }

        private static string GetEmailContent(string subject, MeetingDTO meetingDTO)
        {
            string HTMLBody = string.Empty;
            HTMLBody =
                $"<h1>Please use below meeting details,</h1><br> " +
                $"<p>Start Time : {meetingDTO.StartTime.ToString()} </p> " +
                $"<p> End Time : {meetingDTO.EndTime.ToString()} </p> " +
                $" <p> Location : {meetingDTO.Location} </p> " +
                $" <p> Description: {meetingDTO.Description} </p> ";

            return HTMLBody;
        }

        public static bool SendDeleteMeetingEmail(List<string> emailIds, Meeting _meeting)
        {
            try
            {
                MailMessage message = new MailMessage();
                foreach (string item in emailIds)
                {
                    message.To.Add(new MailAddress(item));

                }
                SmtpClient smtp = new SmtpClient();
                StringBuilder strBody = new StringBuilder();
                message.Subject = "Canceled: " + _meeting.Subject;
                message.From = new MailAddress("dotnetfortegroup11@gmail.com");
                message.IsBodyHtml = true;
                message.Body = GetDeleteMeetingEmailContent(_meeting.Subject, _meeting);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dotnetfortegroup11@gmail.com", "kyet xiue pqla vaxo");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return false;
        }

        private static string GetDeleteMeetingEmailContent(string subject, Meeting _meeting)
        {
            string HTMLBody = string.Empty;
            HTMLBody =
                $"<h1>" + subject + " meeting is canceled</h1><br> " +
                $"<p>Start Time : {_meeting.StartTime.ToString()} </p> " +
                $"<p> End Time : {_meeting.EndTime.ToString()} </p> " +
                $" <p> Location : {_meeting.Location} </p> " +
                $" <p> Description: {_meeting.Description} </p> ";

            return HTMLBody;
        }

    }
}
