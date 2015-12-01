using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Logic.Exceptions
{
    public class UrlUnreachableException : Exception
    {
        public UrlUnreachableException(Exception innerException) : base(Resources.ExceptionMessages.UrlUnreachableException, innerException)
        {

        }
    }
}
