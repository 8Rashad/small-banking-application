namespace TaskApi.Response
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public ResponseModel()
        {
            Errors = new List<string>();
        }

        public static ResponseModel<T> Success(T data, int statusCode = 200)
        {
            return new ResponseModel<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ResponseModel<T> Success(int statusCode = 200)
        {
            return new ResponseModel<T>
            {
                IsSuccess = true,
                Data = default,
                StatusCode = statusCode
            };
        }

        public static ResponseModel<T> Error(List<string> errors, int statusCode = 400)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static ResponseModel<T> Error(string error, int statusCode = 400)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Errors = new List<string> { error },
                StatusCode = statusCode
            };
        }
    }

}
