using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ICYL.BL
{

    [Serializable]
    public partial class ICYLErrorLog : HandleErrorInfo
    {
        public ICYLErrorLog(Exception exception, string controllerName, string actionName) : base(exception, controllerName, actionName)
        {
        }
        public long ErrorId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorTrace { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
