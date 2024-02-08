using BlazorCARS.HealthShield.WebApp.Enums;
using System.Security.AccessControl;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model
{
    public class APIRequest
    {
        public ApiType RequestType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
