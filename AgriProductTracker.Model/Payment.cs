using AgriProductTracker.Model.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class Payment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        //payment via mobile number
        public string MobileNumber { get; set; }
        public int MobileOperator { get; set; }
        //payment via credit card
        public CardType CardType { get; set; }
        public int CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public DateTime Expiration { get; set; }
        public int CVV { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }


    }
}
