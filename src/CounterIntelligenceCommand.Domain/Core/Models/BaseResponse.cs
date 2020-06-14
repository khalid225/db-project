using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace CounterIntelligenceCommand.Domain.Core
{
    public class BaseResponse
    {
        public BaseResponse(bool status, string code, string message, string field = "")
        {
            Status = status;
            Code = code;
            Message = message;
            Field = field;
        }



        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("field")]
        public string Field { get; set; }

    }


    public enum ResponseCode
    {
        Successfull = 10,

        StaffExists = 11,

        InvalidStaffCode = 12,

        InvalidDate = 13,

        UnknownError = 30
    }
}
