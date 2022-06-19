using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.BL
{
    public  class LookupValueBL
    {
        public LookupValueBL()
        {
            this.lookupGrouplst = new List<LookupGroupBL>();
            this.lookupGroup = new LookupGroupBL();
        }
        public virtual int ValueId { get; set; }
        [Required]
        [Display(Name = "Group")]
        public virtual Nullable<int> GroupId { get; set; }
        [Required]
        public virtual string Value { get; set; }
        [Required]
        [Display(Name ="Description")]
        public virtual string ValueDescription { get; set; }
        public virtual bool Active { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual Nullable<System.DateTime> CreatedOn { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual Nullable<System.DateTime> ModifiedOn { get; set; }
        [Display(Name = "Display Order")]
        public virtual int DisplayOrder { get; set; }
        [Required]
        [Display(Name = "API Login ID")]
        public string APIId { get; set; }
        [Required]
        [Display(Name = "Transaction Key")]
        public string APIKey { get; set; }
        public List<LookupGroupBL> lookupGrouplst { get; set; }
        public virtual LookupGroupBL lookupGroup { get; set; }
    }

    public sealed class LookupValueBLList
    {
        public LookupValueBLList()
        {
            this.lookupGrouplst = new List<LookupGroupBL>();
            this.lookupValuelst = new List<LookupValueBL>();
        }
        public int ValueId { get; set; }
        [Required]
        public Nullable<int> GroupId { get; set; }
        public string GroupName { get; set; }
        public string Value { get; set; }
        public string ValueDescription { get; set; }
        public List<LookupGroupBL> lookupGrouplst { get; set; }

        public List<LookupValueBL> lookupValuelst { get; set; }


    }
}
