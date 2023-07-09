using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Shared.Exceptions
{

    public abstract class InternalServerErrorException : AppExceptionBase
    {
        protected InternalServerErrorException(string? message)
            : base(500, message)
        {
        }

        protected InternalServerErrorException(string? message, Exception? innerException)
            : base(500, message, innerException)
        {
        }
    }
}
