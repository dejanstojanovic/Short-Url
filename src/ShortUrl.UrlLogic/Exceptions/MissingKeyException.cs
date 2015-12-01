using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Logic.Exceptions
{
   public class MissingKeyException: Exception
    {
        public MissingKeyException(Exception innerException) : base(Resources.ExceptionMessages.MissingKeyException, innerException)
        {

        }
    }
}
