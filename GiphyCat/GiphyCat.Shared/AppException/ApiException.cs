namespace GiphyCat.AppException
{
    internal class ApiException : System.Exception
    {
        public ApiException(string msg) : base(msg)
        {
        }
    }
}