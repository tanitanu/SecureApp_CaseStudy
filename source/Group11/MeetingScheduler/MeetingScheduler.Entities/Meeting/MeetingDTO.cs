using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Entities
{
    public class MeetingDTO
    {
        public int MeetingId { get; set; }
        [Required(ErrorMessage = "This Title Field is Required")]
        public string? Subject { get; set; }
        public string? Location { get; set; }
        [DateValidation(ErrorMessage = "The StartTime should not be lesser than current DateTime.")]
        public DateTime? StartTime { get; set; }
        [EndDateValidation(ErrorMessage = "The EndTime should be lesser than StartTime.")]
        
        public DateTime? EndTime { get; set; }

        public string? Description { get; set; }

        public bool? IsAllDay { get; set; }

        public string? RecurrenceRule { get; set; }

        public string? RecurrenceException { get; set; }

        public int? RecurrenceId { get; set; }

        public int MeetingCreatedUserId { get; set; }

       public string LastUpdatedUserId {  get; set; }
       
        [MultiEmail(ErrorMessage = "Please insert a Valid Email Address")]
        public string? emailAddresses {  get; set; }
        public string? UserName { get; set; }

        /// <summary>
        /// Customer Validator
        /// </summary>
        public static DateTime StartDateTime;
        public static string EmailErrorMessage="knkn";
        public class DateValidationAttribute : ValidationAttribute
        {
          
            public override bool IsValid(object value)
            {
                DateTime startTime = Convert.ToDateTime(value);
                StartDateTime = startTime;
                return startTime >= DateTime.Now;
            }
        }
        public class EndDateValidationAttribute : ValidationAttribute
        {
            
            public override bool IsValid(object value)
            {
                DateTime todayDate = Convert.ToDateTime(value);
                return todayDate >= StartDateTime;
            }
        }
        public class MultiEmailAttribute : ValidationAttribute
        {
            private bool ValidEmail = false;

            public override bool IsValid(object value)
            {
                string[] emails = value?.ToString().Split(';');
                if (emails!=null && emails.Length!=0)
                {
                    
                    foreach (var email in emails)
                    {
                        if (IsValidEmail(email.Trim()))
                        {
                            ValidEmail=true;
                        }
                        else
                        {
                            EmailErrorMessage = "Please enter a valid email address";
                            ValidEmail=false;
                        }
                    }
                }
                EmailErrorMessage = "The Email Field is required";

                return ValidEmail;

            }
            static bool IsValidEmail(string email)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        var mailAddress = new MailAddress(email);
                        return true;
                    }
                    return false;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
        }

    }

    
}

    




