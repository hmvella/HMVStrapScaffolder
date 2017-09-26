//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using StatusUpdater.Models;
//using StatusUpdater.Attribute;
//using System.Dynamic;
//using Newtonsoft.Json;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//using StatusUpdater.Models;

//namespace StatusUpdater.Controllers
//{
//    public class SalesFrontendAudit_MetaController : Controller
//    {
//        private HMVTestEntities db = new HMVTestEntities();

//        // GET: SalesFrontendAudit_Meta
//        public ActionResult Index()
//        {
//            return View(db.SalesFrontendAudit_Meta.ToList());
//        }

//        // GET: SalesFrontendAudit_Meta/Details/5
//        public ActionResult Details(long? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SalesFrontendAudit_Meta salesFrontendAudit_Meta = db.SalesFrontendAudit_Meta.Find(id);
//            if (salesFrontendAudit_Meta == null)
//            {
//                return HttpNotFound();
//            }
//            return View(salesFrontendAudit_Meta);
//        }

//        // GET: SalesFrontendAudit_Meta/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Bootstrap Table Functions

//        [Audit]
//        public ContentResult GetTableData(int inTake = 50, int inSkip = 1)
//        {
//            dynamic expandoObject = new ExpandoObject();

//            List<SalesFrontendAudit_Meta> lstSalesFrontendAudit_Meta = db.SalesFrontendAudit_Meta.ToList<SalesFrontendAudit_Meta>();

//            expandoObject.total = lstSalesFrontendAudit_Meta.Count();
//            expandoObject.rows = lstSalesFrontendAudit_Meta;

//            string json = JsonConvert.SerializeObject(expandoObject);
//            ContentResult result = Content(json);
//            return result;
//        }

//        [Audit]
//        public ContentResult GetTableData(int inTake = 50, int inSkip = 0, string inSearch = "%", string inOrder = "Id", string inOrderBy = "asc")
//        {
//            List<string> lstPropNames = new List<string>();
//            List<SalesFrontendAudit_Meta> lstSalesFrontendAudit_Meta;

//            dynamic expandoObject = new ExpandoObject();

//            if (inSearch.Equals("%"))
//            {
//                lstSalesFrontendAudit_Meta = (from itm in db.SFESoAudits
//                                              select new SalesFrontendAudit_Meta
//                                              {
//                                                  Id = itm.Id
//  ,
//                                                  CstCode = itm.CstCode
//  ,
//                                                  EstNo = itm.EstNo
//  ,
//                                                  OrdNo = itm.OrdNo
//  ,
//                                                  DelType = itm.DelType
//  ,
//                                                  SOType = itm.SOType
//  ,
//                                                  DelDesc = itm.DelDesc
//  ,
//                                                  CreatedBy = itm.CreatedBy
//  ,
//                                                  DateCreated = itm.DateCreated
//  ,
//                                                  LineNo = itm.LineNo
//  ,
//                                                  AgreementNo = itm.AgreementNo
//  ,
//                                                  IsProspective = itm.IsProspective
//                                              }
//                                          ).OrderBy(f => f.id).Skip(inSkip).Take(inTake).ToList<SalesFrontendAudit_Meta>();
//            }
//            else
//            {
//                lstSalesFrontendAudit_Meta = (from itm in db.SFESoAudits
//                                              where
//                                              (
//                                              itm.Id.ToString()
//  + itm.CstCode.ToString()
//  + itm.EstNo.ToString()
//  + itm.OrdNo.ToString()
//  + itm.DelType.ToString()
//  + itm.SOType.ToString()
//  + itm.DelDesc.ToString()
//  + itm.CreatedBy.ToString()
//  + itm.DateCreated.ToString()
//  + itm.LineNo.ToString()
//  + itm.AgreementNo.ToString()
//  + itm.IsProspective.ToString()

//                                              ).Contains(inSearch)
//                                              select new SalesFrontendAudit_Meta
//                                              {
//                                                  Id = itm.Id
//  ,
//                                                  CstCode = itm.CstCode
//  ,
//                                                  EstNo = itm.EstNo
//  ,
//                                                  OrdNo = itm.OrdNo
//  ,
//                                                  DelType = itm.DelType
//  ,
//                                                  SOType = itm.SOType
//  ,
//                                                  DelDesc = itm.DelDesc
//  ,
//                                                  CreatedBy = itm.CreatedBy
//  ,
//                                                  DateCreated = itm.DateCreated
//  ,
//                                                  LineNo = itm.LineNo
//  ,
//                                                  AgreementNo = itm.AgreementNo
//  ,
//                                                  IsProspective = itm.IsProspective
//                                              }
//                                        ).OrderBy(f => f.Id).Skip(inSkip).Take(inTake).ToList<SalesFrontendAudit_Meta>();
//            }

//            expandoObject.total = lstSalesFrontendAudit_Meta.Count();
//            expandoObject.rows = lstSalesFrontendAudit_Meta;

//            string json = JsonConvert.SerializeObject(expandoObject);
//            ContentResult result = Content(json);
//            return result;
//        }

//        #endregion Bootstrap Table Functions
//    }
//}