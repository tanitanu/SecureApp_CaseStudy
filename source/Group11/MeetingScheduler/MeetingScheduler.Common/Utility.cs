using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Common
{
    public static class Utility
    {
        /// <summary>
        /// Returns the method start logging message
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static string GetStartMessage(string MethodName)
        {
            return ("Started " + MethodName + " service method");
        }

        /// <summary>
        /// Returns the method end logging message
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static string GetEndMessage(string MethodName)
        {
            return ("End " + MethodName + " service method");
        }

        /// <summary>
        /// Get Exception Message
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(string MethodName)
        {
            return ("Exception in " + MethodName + " service method");
        }

        /// <summary>
        /// Get unexpected exception message
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static string GetUnexpectedExceptionMessage(string MethodName)
        {
            return ("Unexpected response from the API in " + MethodName +" method");
        }

        
    }
}
