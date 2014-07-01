using System;

namespace GiphyCat.Exceptions
{
    internal class ApiException : Exception
    {
        public ApiException(string msg) : base(msg)
        {
        }
    }
}