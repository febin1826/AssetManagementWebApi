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
    public class Asset_TypeController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        // GET: api/Asset_Type
        public IQueryable<asset_type> Getasset_type()
        {
            return db.asset_type;
        }

        // GET: api/Asset_Type/5
        [ResponseType(typeof(asset_type))]
        public IHttpActionResult Getasset_type(decimal id)
        {
            asset_type asset_type = db.asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return Ok(asset_type);
        }
        public Asset_TypeController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public List<asset_type> Getasset_type(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<asset_def> ad = db.asset_def.Where(x => x.ad_name == name).ToList();
            List<asset_type> list = ad.Select(x => new asset_type
            {
                at_id = Convert.ToInt32(x.ad_type_id),
                at_name = x.asset_type.at_name
            }).ToList();
            return list;
        }
        // PUT: api/Asset_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_type(decimal id, asset_type asset_type)
        {

            if (id != asset_type.at_id)
            {
                return BadRequest();
            }

            db.Entry(asset_type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_typeExists(id))
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

        // POST: api/Asset_Type
        [ResponseType(typeof(asset_type))]
        public IHttpActionResult Postasset_type(asset_type asset_type)
        {
           

            db.asset_type.Add(asset_type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_type.at_id }, asset_type);
        }

        // DELETE: api/Asset_Type/5
        [ResponseType(typeof(asset_type))]
        public IHttpActionResult Deleteasset_type(decimal id)
        {
            asset_type asset_type = db.asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            db.asset_type.Remove(asset_type);
            db.SaveChanges();

            return Ok(asset_type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_typeExists(decimal id)
        {
            return db.asset_type.Count(e => e.at_id == id) > 0;
        }
    }
}