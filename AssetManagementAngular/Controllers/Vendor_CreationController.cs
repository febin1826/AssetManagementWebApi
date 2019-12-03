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
    public class Vendor_CreationController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        //// GET: api/Vendor_Creation
        //public IQueryable<vendor_creation> Getvendor_creation()
        //{
        //    return db.vendor_creation;
        //}
        public Vendor_CreationController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        public List<VendorViewModel> GetVendor()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<vendor_creation> vendorList = db.vendor_creation.ToList();
            List<VendorViewModel> viewList = vendorList.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type =x.vd_type,
                vd_atype = x.asset_type.at_name,
                vd_fromStr = x.vd_fromStr,
                vd_toStr=x.vd_toStr,
                vd_addr=x.vd_addr

            }).ToList();
            return viewList;
        }

        // GET: api/Vendor_Creation/5
        [ResponseType(typeof(vendor_creation))]
        public IHttpActionResult Getvendor_creation(decimal id)
        {
            vendor_creation vendor_creation = db.vendor_creation.Find(id);
            if (vendor_creation == null)
            {
                return NotFound();
            }

            return Ok(vendor_creation);
        }

        // PUT: api/Vendor_Creation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putvendor_creation(decimal id, vendor_creation vendor_creation)
        {

            if (id != vendor_creation.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendor_creation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vendor_creationExists(id))
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

        // POST: api/Vendor_Creation
        [ResponseType(typeof(vendor_creation))]
        public int Postvendor(vendor_creation vendor)
        {
            vendor_creation vd = new vendor_creation();
            vd = db.vendor_creation.Where(x => x.vd_name == vendor.vd_name && x.vd_atype_id == vendor.vd_atype_id).FirstOrDefault();
            if (vd == null)
            {
                db.vendor_creation.Add(vendor);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }


          
        }

        // DELETE: api/Vendor_Creation/5
        [ResponseType(typeof(vendor_creation))]
        public IHttpActionResult Deletevendor_creation(decimal id)
        {
            vendor_creation vendor_creation = db.vendor_creation.Find(id);
            if (vendor_creation == null)
            {
                return NotFound();
            }

            db.vendor_creation.Remove(vendor_creation);
            db.SaveChanges();

            return Ok(vendor_creation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vendor_creationExists(decimal id)
        {
            return db.vendor_creation.Count(e => e.vd_id == id) > 0;
        }
    }
}