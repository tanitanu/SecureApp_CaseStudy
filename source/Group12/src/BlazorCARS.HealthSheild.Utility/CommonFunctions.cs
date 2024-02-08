using BlazorCARS.HealthShield.Utility.Extensions;
using BlazorCARS.HealthShield.Utility.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.Utility
{
    public class CommonFunctions
    {
        public static bool ValidatePaginationParameters(APIResponse respsonse, int pageSize, int pageNumber)
        {
            string result = pageSize.GreaterThan(0, "Page Size");
            if (result != string.Empty)
            {
                respsonse.ErrorMessages.Add(result);
            }
            result = pageNumber.GreaterThan(0, "Page Number");
            if (result != string.Empty)
            {
                respsonse.ErrorMessages.Add(result);
            }
            if (respsonse.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static async Task<string> UploadImage(IFormFile imageToUpload, string fileName = null, int recId = 0)
        {
            string filePath;
            string fileOldPath;

            if (fileName != null && recId > 0)
            {
                fileOldPath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Archive", $"{recId}_{fileName.Replace("Resources\\Images\\", "")}");
                filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, fileOldPath);
                }
            }
            fileName = Guid.NewGuid() + Path.GetExtension(imageToUpload.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await imageToUpload.CopyToAsync(fileStream);
            string returnRelativePath = Path.Combine(@"Resources\Images", fileName);
            return returnRelativePath;
        }
        //Password encryption
        public static string EncryptPassword(string password)
        {
            if (password.NullOrWhiteSpace("Password") != string.Empty)
            {
                return string.Empty;
            }
            password += "SRACrozalBBlazorCARSNetFortFullStackenOhctaB";
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(passwordInBytes);
        }
        //Password decryption
        public static string DecryptPassword(string encryptedPassword)
        {
            if (encryptedPassword.NullOrWhiteSpace("Password") != string.Empty)
            {
                return string.Empty;
            }
            var encodedBytes = Convert.FromBase64String(encryptedPassword);
            var actualPassword = Encoding.UTF8.GetString(encodedBytes);
            actualPassword = actualPassword.Substring(0, actualPassword.Length - "SRACrozalBBlazorCARSNetFortFullStackenOhctaB".Length);
            return actualPassword;
        }
    }
}
