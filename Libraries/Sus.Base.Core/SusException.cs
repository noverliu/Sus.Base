using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sus.Base.Core
{
    [Serializable]
    public class SusException:Exception
    {
        public SusException()
        {
        }
        public SusException(string message)
            : base(message)
        {
        }
        public SusException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }
        public SusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
