using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnterpriseLayer.Utilities;
using ICYL.BL;
using ICYL.Repository;

namespace ICYL.Web.Controllers
{
    [Authorize]
    public class LookupController : BaseController
    {
        // GET: Lookup
        public ActionResult Index()
        {
            return View("LookupList");
        }

        public ActionResult LookupList()
        {

            LookupValueBLList model = new LookupValueBLList();
            model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return View("LookupList", model);
        }


        [ActionName("LookupListPost")]
        public ActionResult LookupList(LookupValueBLList model)
        {
            if (ModelState.IsValid)
            {
                model.lookupValuelst = new LookupRepository().getLookupList(model);
            }
            model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return View("LookupList", model);
        }

        public ActionResult NewLookup(int id,int groupId=0)
        {

            LookupValueBL model = new LookupValueBL();
            if (id > 0)
            {
                model = new LookupRepository().GetLookupValueById(id);
                ViewBag.Readonly = true;
            }
            else
            {
                ViewBag.Readonly = false;
                model.DisplayOrder = new LookupRepository().GetNextDisplayOrder(groupId);

            }

            model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return PartialView("_AddLookup", model);
        }

        [HttpPost]
        [ActionName("NewLookupPost")]
        public ActionResult NewLookup(LookupValueBL lookup)
        {

            bool Status = false;
            if (ModelState.IsValid)
            {
                int rValue = new LookupRepository().SaveLookup(lookup);
                if (rValue > 0) Status = true;
                return new JsonResult { Data = new { status = Status } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }

        [HttpPost]
        public ActionResult UpdateLookup(LookupValueBL lookup)
        {

            bool Status = false;
            if (ModelState.IsValid)
            {
                int rValue =  new LookupRepository().UpdateLookup(lookup);
                if (rValue > 0) Status = true;
                return new JsonResult { Data = new { status = Status } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }

        [ActionName("Delete")]
        public ActionResult Deletelookup(int id,string strActive)
        {
            bool isActive = false;
            bool status = false;
            isActive =strActive.ToLower().Equals("true")? true : false;

            int rValue= new LookupRepository().DeleteLookup(id,Conversion.ConversionToBool(isActive));
            if (rValue > 0) status = true;
            return new JsonResult { Data = new { status = status } };

        }


        public ActionResult LookupCategoryList()
        {
            LookupValueBLList model = new BL.LookupValueBLList();
            if (ModelState.IsValid)
            {
                model.lookupValuelst = new LookupRepository().getCategoryLookupList();
            }
            model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return View("LookupCategoryList", model);
        }


        public ActionResult LookupEmailList()
        {
            //LookupValueBLList model = new BL.LookupValueBLList();
            //if (ModelState.IsValid)
            //{
            //    model.lookupValuelst = new LookupRepository().getCategoryLookupList();
            //}
            //model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return View();
        }

        public ActionResult NewLookupCategory(int id, int groupId = 0)
        {

            LookupValueBL model = new LookupValueBL();
            if (id > 0)
            {
                model = new LookupRepository().GetLookupValueById(id);
                ViewBag.Readonly = true;
            }
            else
            {
                ViewBag.Readonly = false;
                model.DisplayOrder = new LookupRepository().GetNextDisplayOrder(groupId);

            }

            model.lookupGrouplst = new LookupRepository().GetLookupGroup();
            return PartialView("_AddCategoryLookup", model);
        }

        [HttpPost]
        [ActionName("NewLookupCategoryPost")]
        public ActionResult NewLookupCategory(LookupValueBL lookup)
        {

            bool Status = false;
            if (ModelState.IsValid)
            {
               int rValue = new LookupRepository().SaveLookup(lookup);
                if (rValue > 0) Status = true;
                return new JsonResult { Data = new { status = Status } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }



    }
}