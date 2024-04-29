using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class ApiResponseModel
    {
        public string? Message { get; set; }
        public bool? Status { get; set; }
        public object? Data { get; set; }
        public ApiResponseModel(string message, bool status, object data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
        public ApiResponseModel()
        {

        }
    }
}