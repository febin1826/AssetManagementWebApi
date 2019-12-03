using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngular.Models
{
    public class PurchaseOrderViewModel
    {
        public decimal pd_id { get; set; }
        public string pd_order_no { get; set; }
        public Nullable<decimal> pd_ad_id { get; set; }
        public string assetname { get; set; }
        //public string pd_ad { get; set; }
        public Nullable<decimal> pd_type_id { get; set; }
        public string assettype { get; set; }

        public int pd_qty { get; set; }
        public Nullable<decimal> pd_vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string pd_odateStr { get; set; }
        public string pd_ddateStr { get; set; }
        public string pd_status { get; set; }
        


    }
}