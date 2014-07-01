using System;

namespace GiphyCat.Exceptions
{
    internal class NetworkException : Exception
    {
        public NetworkException(string error) : base(error)
        {
        }
    }
}