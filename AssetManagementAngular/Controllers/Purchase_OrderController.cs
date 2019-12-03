using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AssetManagementAngular.Models;

namespace AssetManagementAngular.Controllers
{
    public class Purchase_OrderController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        // GET: api/Purchase_Order
        //public IQueryable<purchase_order> Getpurchase_order()
        //{
        //    return db.purchase_order;
        //}
        public Purchase_OrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        public List<PurchaseOrderViewModel> GetPurchase_Order()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<purchase_order> pList = db.purchase_order.ToList();
            List<PurchaseOrderViewModel> prList = pList.Select(x => new PurchaseOrderViewModel
            {
                pd_id = x.pd_id,
                pd_order_no = x.pd_order_no,
                assetname = x.asset_def.ad_name,
                assettype = x.asset_type.at_name,
                pd_qty=Convert.ToInt32(x.pd_qty),
                vendor_name = x.vendor_creation.vd_name,
                pd_odateStr = x.pd_odateStr,
                pd_ddateStr=x.pd_ddateStr,
                pd_status=x.pd_status

            }).ToList();
            return prList;
        }
        public List<VendorViewModel> Getvendor_creation(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<vendor_creation> vendorList = db.vendor_creation.Where(x=>x.vd_atype_id==id).ToList();
            List<VendorViewModel> viewList = vendorList.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id=x.vd_atype_id,
                vd_atype= x.asset_type.at_name,
                vd_fromStr = x.vd_fromStr,
                vd_toStr = x.vd_toStr,
                vd_addr = x.vd_addr

            }).ToList();
            return viewList;
        }
        public List<asset_type> Getasset_types(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            asset_def ad = db.asset_def.Where(x => x.ad_name == name).FirstOrDefault();
            List<asset_type> list = new List<asset_type>();
            if(ad!=null)
            {
                list = db.asset_type.Where(x => x.at_id == ad.ad_type_id).ToList();
            }
           
            
            return list;
        }
        //public List<AssetViewModel> GetAssetTypes(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = true;
        //    List<asset_def> assetList = db.asset_def.Where(x => x.ad_type_id == id).ToList();
        //    List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
        //    {
        //        ad_id = Convert.ToInt32(x.ad_id),
        //        ad_name = x.ad_name,

        //        ad_type = x.asset_type.at_name,
        //        ad_class = x.ad_class,

        //    }).ToList();
        //    return avList;
        //}
        // GET: api/Purchase_Order/5
        //[ResponseType(typeof(purchase_order))]
        //public IHttpActionResult Getpurchase_order(decimal id)
        //{
        //    purchase_order purchase_order = db.purchase_order.Find(id);
        //    if (purchase_order == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(purchase_order);
        //}

        // PUT: api/Purchase_Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpurchase_order(decimal id, purchase_order purchase_order)
        {
           

            if (id != purchase_order.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchase_order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!purchase_orderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Purchase_Order
        [ResponseType(typeof(purchase_order))]
        public IHttpActionResult Postpurchase_order(purchase_order purchase_order)
        {

            purchase_order.pd_odate = DateTime.Now;
            db.purchase_order.Add(purchase_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/Purchase_Order/5
        [ResponseType(typeof(purchase_order))]
        public IHttpActionResult Deletepurchase_order(decimal id)
        {
            purchase_order purchase_order = db.purchase_order.Find(id);
            if (purchase_order == null)
            {
                return NotFound();
            }

            db.purchase_order.Remove(purchase_order);
            db.SaveChanges();

            return Ok(purchase_order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool purchase_orderExists(decimal id)
        {
            return db.purchase_order.Count(e => e.pd_id == id) > 0;
        }
    }
}