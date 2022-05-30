using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Results
{
    public class ValidatorDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public ValidatorDto(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
