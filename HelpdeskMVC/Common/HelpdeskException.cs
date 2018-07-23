using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplWithSql.Common
{
    public class HelpdeskException :  Exception
    {
        public HelpdeskException():base()
        {
        }

        public HelpdeskException(string message)
            : base(message)
        {
        }

        public HelpdeskException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}