
namespace GiphyCat.AppException
{
    internal class NetworkException : System.Exception
    {
        public NetworkException(string error) : base(error)
        {
        }
    }
}