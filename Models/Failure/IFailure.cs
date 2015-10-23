using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainFree.Models
{
    interface IFailure
    {
        Boolean IsFailure { get; }
        String FailureMessage { get; }
    }
}
