using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Exceptions
{
    public class CustomExceptions : Exception
    {
        public CustomExceptions():base()
        {
        }


        public CustomExceptions(string message):base(message)
        {
            
        }

        public CustomExceptions(string message,Exception innerException):base(message, innerException) 
        {
            
        }
    }
}
