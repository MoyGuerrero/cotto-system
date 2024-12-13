namespace cotto_system.Modelos
{
    public class ResponseData<TData>
    {
        public Boolean Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public TData Data { get; set; }
        public string Token { get; set; }

        public ResponseData(Boolean ok, string message, int statusCode, TData data, string token)
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
        public Boolean Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Success(Boolean ok, string message, int statusCode)
        {
            Ok = ok;
            Message = message;
            StatusCode = statusCode;
        }
    }

    public class SuccessWithData <TData>
    {
        public Boolean Ok { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public TData Data { get; set; }
        public SuccessWithData(Boolean ok, string message, int statusCode,TData data)
        {
            Ok = ok;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }
    }
}
