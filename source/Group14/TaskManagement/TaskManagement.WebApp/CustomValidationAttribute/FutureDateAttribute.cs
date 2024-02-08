#region [Using]
using System.ComponentModel.DataAnnotations;
#endregion [Using]

namespace TaskManagement.WebApp.CustomValidationAttribute
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>09-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>FutureValidationAttribute</class>
    /// <summary>
    /// This is the Future Validation for Task properties
    /// </summary>
    #endregion [Summary]
    public class FutureDateAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }

            return false;
        }
    }
   
}
