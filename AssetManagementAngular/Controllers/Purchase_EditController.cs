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
    public class Purchase_EditController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        // GET: api/Purchase_Edit
        public IQueryable<purchase_order> Getpurchase_order()
        {
            return db.purchase_order;
        }
        public Purchase_EditController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Purchase_Edit/5
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
        [ResponseType(typeof(purchase_order))]
        public PurchaseOrderViewModel GetPurchaseOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            purchase_order order = db.purchase_order.Where(x => x.pd_id == id).FirstOrDefault();
            PurchaseOrderViewModel model = new PurchaseOrderViewModel();
            model.pd_id = order.pd_id;
            model.pd_order_no = order.pd_order_no;
            model.assetname = order.asset_def.ad_name;
            model.assettype = order.asset_type.at_name;
            model.pd_ad_id = order.pd_ad_id;
            model.pd_type_id = order.pd_type_id;
            model.pd_vendor_id = order.pd_vendor_id;
            model.vendor_name = order.vendor_creation.vd_name;
            model.pd_odateStr = order.pd_odateStr;
            model.pd_ddateStr = order.pd_ddateStr;
            model.pd_qty = Convert.ToInt32(order.pd_qty);
            model.pd_status = order.pd_status;
           
            return model;



        }

        // PUT: api/Purchase_Edit/5
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

        // POST: api/Purchase_Edit
        [ResponseType(typeof(purchase_order))]
        public IHttpActionResult Postpurchase_order(purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.purchase_order.Add(purchase_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/Purchase_Edit/5
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