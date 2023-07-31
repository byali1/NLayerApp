using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        //Json'a convert ederken ignore et. Client'a dönmücez kod içinde lazım.
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }


        //Static Factory Methods

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                //Errors = null (default null olacak zaten)
            };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
            };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }

        public static CustomResponseDto<T> Fail(int statusCode,string error)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = new List<string>{error}
            };
        }
    }
}
