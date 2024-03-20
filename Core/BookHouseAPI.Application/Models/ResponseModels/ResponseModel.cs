using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Models.ResponseModels
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseModel()
        {
            Success = false;
        }
    }
}
