using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinSharp.Api.Exceptions
{
    public class PinSharpServerErrorException : PinSharpException
    {
        public PinSharpServerErrorException()
        {

        }

        public PinSharpServerErrorException(string message)
            : base(message)
        {

        }

        public PinSharpServerErrorException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
