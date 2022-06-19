using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.BL
{
    public class PaymentBatch
    {
        public int BatchId { get; set; } 
        public DateTime settlementTimeUTC { get; set; }  
        public DateTime settlementTimeLocal { get; set; }  
        public string settlementState { get; set; }
        public string marketType   { get; set; }
        public string product { get; set; }
}
}
