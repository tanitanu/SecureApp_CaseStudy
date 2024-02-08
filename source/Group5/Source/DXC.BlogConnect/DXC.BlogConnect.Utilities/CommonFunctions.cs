using System.Text;

namespace DXC.BlogConnect.WebAPI.Utilities
{
    public class CommonFunctions
    {
        //Password encryption
        public static string EncryptPassword(string password)
        {
            password += "BloGConnectDXCuSerSCopyrightS@2023";
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordInBytes);
        }
        //Password decryption
        public static string DecryptPassword(string encryptedPassword)
        {
            var encodedBytes = Convert.FromBase64String(encryptedPassword);
            var actualPassword = Encoding.UTF8.GetString(encodedBytes);
            actualPassword = actualPassword.Substring(0, actualPassword.Length - "BloGConnectDXCuSerSCopyrightS@2023".Length);
            return actualPassword;
        }
    }
}
