using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngular.Models
{
    public class VendorViewModel
    {
        public decimal vd_id { get; set; }
        public string vd_name { get; set; }
        public string vd_type { get; set; }
        public string vd_atype { get; set; }
        public Nullable<decimal> vd_atype_id { get; set; }
        //public DateTime vd_from { get; set; }
        public string vd_fromStr { get; set; }

        // public DateTime vd_to { get; set; }
        public string vd_toStr { get; set; }

        public string vd_addr { get; set; }
    }
}