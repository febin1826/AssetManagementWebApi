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
    public class asset_defController : ApiController
    {
        private assetMVCEFDBEntities6 db = new assetMVCEFDBEntities6();

        // GET: api/asset_def
        //public IQueryable<asset_def> Getasset_def()
        //{
        //    return db.asset_def;
        //}

        // GET: api/asset_def/5
        [ResponseType(typeof(asset_def))]
        public IHttpActionResult Getasset_def(decimal id)
        {
            asset_def asset_def = db.asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            return Ok(asset_def);
        }
        public List<asset_def> Getasset(string name)
        {
            List<asset_def> plist = db.asset_def.Where(x => x.ad_name.StartsWith(name)).ToList();
            return plist;
        }

        // PUT: api/asset_def/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_def(decimal id, asset_def asset_def)
        {
            

            if (id != asset_def.ad_id)
            {
                return BadRequest();
            }

            db.Entry(asset_def).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_defExists(id))
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
        public asset_defController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        public List<AssetViewModel> Getasset_def(string na)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<asset_def> assetList = db.asset_def.Where(x=>x.ad_name.StartsWith(na)).ToList();
            List<AssetViewModel> viewList = assetList.Select(x => new AssetViewModel
            {
                ad_id = x.ad_id,
                ad_name = x.ad_name,
                ad_type = x.asset_type.at_name,
                ad_class = x.ad_class

            }).ToList();
            return viewList;




        }
        public List<AssetViewModel> GetAsset()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<asset_def> assetList = db.asset_def.ToList();
                List<AssetViewModel> viewList = assetList.Select(x => new AssetViewModel
                {
                    ad_id = x.ad_id,
                    ad_name = x.ad_name,
                    ad_type = x.asset_type.at_name,
                    ad_class = x.ad_class

                }).ToList();
                return viewList;
            



        }

        // POST: api/asset_def
        [ResponseType(typeof(asset_def))]
        public int Postasset_def(asset_def asset_def)
        {

            asset_def asset = new asset_def();
            asset = db.asset_def.Where(x => x.ad_name == asset_def.ad_name && x.ad_type_id == asset_def.ad_type_id && x.ad_class == asset_def.ad_class).FirstOrDefault();
            if (asset == null)
            {
                db.asset_def.Add(asset_def);
                db.SaveChanges();
                return 0;

            }
            else
            {
                return 1;
            }
        }
       
    

        // DELETE: api/asset_def/5
        [ResponseType(typeof(asset_def))]
        public IHttpActionResult Deleteasset_def(decimal id)
        {
            asset_def asset_def = db.asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            db.asset_def.Remove(asset_def);
            db.SaveChanges();

            return Ok(asset_def);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_defExists(decimal id)
        {
            return db.asset_def.Count(e => e.ad_id == id) > 0;
        }
    }
}