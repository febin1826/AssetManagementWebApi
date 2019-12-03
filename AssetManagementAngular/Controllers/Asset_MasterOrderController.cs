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
    public class Asset_MasterOrderController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        Asset_MasterOrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Asset_MasterOrder
        //public IQueryable<asset_master> Getasset_master()
        //{
        //    return db.asset_master;
        //}

        // GET: api/Asset_MasterOrder/5
        // GET: api/AssetMasterOrderView
        public List<PurchaseOrderViewModel> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<purchase_order> pList = db.purchase_order.Where(x => x.pd_status == "Consignment Received").ToList();
            List<PurchaseOrderViewModel> pvList = pList.Select(x => new PurchaseOrderViewModel
            {
                pd_id = x.pd_id,
                pd_order_no = x.pd_order_no,
                pd_ad_id = x.asset_def.ad_id,
                assetname = x.asset_def.ad_name,
                pd_odateStr = x.pd_odateStr,
                pd_ddateStr = x.pd_ddateStr,
                pd_qty = Convert.ToInt32(x.pd_qty),
                pd_status = x.pd_status,
                pd_type_id = x.asset_type.at_id,
                assettype = x.asset_type.at_name,
                pd_vendor_id = x.pd_vendor_id,
                vendor_name = x.vendor_creation.vd_name



            }).ToList();

            return pvList;
        }

        // GET: api/AssetMasterOrderView/5
        [ResponseType(typeof(asset_master))]
        public PurchaseOrderViewModel GetAsset_master(string ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            purchase_order x = db.purchase_order.Where(y => y.pd_order_no == ordno).FirstOrDefault();
            PurchaseOrderViewModel pvModel = new PurchaseOrderViewModel();

            if (x == null)
            {

            }

            else
            {
                pvModel.pd_id = x.pd_id;
                pvModel.pd_order_no = x.pd_order_no;
                pvModel.pd_ad_id = x.asset_def.ad_id;
                pvModel.assetname = x.asset_def.ad_name;
                pvModel.pd_odateStr = x.pd_odateStr;
                pvModel.pd_ddateStr = x.pd_ddateStr;
                pvModel.pd_qty = Convert.ToInt32(x.pd_qty);
                pvModel.pd_status = x.pd_status;
                pvModel.pd_type_id = x.asset_type.at_id;
                pvModel.assettype = x.asset_type.at_name;
                pvModel.pd_vendor_id = x.pd_vendor_id;
                pvModel.vendor_name = x.vendor_creation.vd_name;
            }

            return pvModel;
        }

        // PUT: api/Asset_MasterOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_master(int id, asset_master asset_master)
        {
           

            if (id != asset_master.am_id)
            {
                return BadRequest();
            }

            db.Entry(asset_master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_masterExists(id))
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

        // POST: api/Asset_MasterOrder
        [ResponseType(typeof(asset_master))]
        public IHttpActionResult Postasset_master(asset_master asset_master)
        {
            

            db.asset_master.Add(asset_master);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/Asset_MasterOrder/5
        [ResponseType(typeof(asset_master))]
        public IHttpActionResult Deleteasset_master(int id)
        {
            asset_master asset_master = db.asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            db.asset_master.Remove(asset_master);
            db.SaveChanges();

            return Ok(asset_master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_masterExists(int id)
        {
            return db.asset_master.Count(e => e.am_id == id) > 0;
        }
    }
}