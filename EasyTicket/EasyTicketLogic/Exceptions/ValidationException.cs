using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicketLogic
{
    public class ValidationException : Exception
    {
        public Dictionary<string, Dictionary<string, string>> ValidationResults { get; private set; }

        public ValidationException(Dictionary<string, Dictionary<string, string>> validationResults)
        {
            ValidationResults = validationResults;
        }
    }
}
