using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Business
{
    public abstract class BusinessBase
    {
        internal ValidateException ValidateException(string message)
        {
            return new ValidateException(message);
        }
    }
}
