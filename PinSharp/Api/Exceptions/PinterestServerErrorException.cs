using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinSharp.Api.Exceptions
{
    public class PinterestServerErrorException : PinterestException
    {
        public PinterestServerErrorException()
        {

        }

        public PinterestServerErrorException(string message)
            : base(message)
        {

        }

        public PinterestServerErrorException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
