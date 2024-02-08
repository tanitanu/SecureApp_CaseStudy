using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureApp.Model.SecureAppModel
{
    public class LoginResponseDto
    {
        public bool IsSuccessfulLogin { get; set; }
        public string Errors { get; set; }
    }
}
