using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataApiModels
{
    public class Message<T>
    {
        [DisplayName("IsSuccess")]
        public bool IsSuccess { get; set; }

        [DisplayName("ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DisplayName("Data")]
        public T Data { get; set; }
    }
}
