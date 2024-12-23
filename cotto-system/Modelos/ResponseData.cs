﻿namespace cotto_system.Modelos
{
    public class ResponseData<TData>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public TData Data { get; set; }
        public string Token { get; set; }

        public ResponseData(bool ok, string message, int statusCode, TData data, string token)
        {
            Ok = ok;
            Message = message;
            StatusCode = statusCode;
            Data = data;
            Token = token;
        }
    }


    public class Success
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Success(bool ok, string message, int statusCode)
        {
            Ok = ok;
            Message = message;
            StatusCode = statusCode;
        }
    }

    public class SuccessWithData<TData>
    {
        public Boolean Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public TData Data { get; set; }
        public SuccessWithData(bool ok, string message, int statusCode, TData data)
        {
            Ok = ok;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }
    }

    public class SuccessWithID
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int ID { get; set; }

        public SuccessWithID(bool ok, string message, int statuscode, int id)
        {
            Ok = ok;
            Message = message;
            StatusCode = statuscode;
            ID = id;
        }
    }
}
