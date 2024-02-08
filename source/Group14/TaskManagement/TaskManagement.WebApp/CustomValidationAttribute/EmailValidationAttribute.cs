#region [Using]
using System.ComponentModel.DataAnnotations;
#endregion [Using]


namespace TaskManagement.WebApp.CustomValidationAttribute
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>09-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>EmailValidationAttribute</class>
    /// <summary>
    /// This is the Email Validation for Task properties
    /// </summary>
    #endregion [Summary]
    public class EmailValidationAttribute:ValidationAttribute
    {
        private readonly string allowedDomain;

        /// <summary>
        /// This is to validate Email 
        /// </summary>
        /// <param name="allowedDomain">"dxc.com"</param>
        public EmailValidationAttribute(string allowedDomain) 
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object? value)
        {
            //return base.IsValid(value);
            string[] strings = Convert.ToString(value).Split("@");
            if (strings.Count() > 1)
                return strings[1].ToUpper() == allowedDomain.ToUpper();
            else
                return false;
        }
    }
}
