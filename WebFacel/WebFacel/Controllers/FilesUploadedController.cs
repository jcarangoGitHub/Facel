using DBE.Net.Data.Basic;
using DBE.Net.Facel.WebFacel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBE.Net.Facel.WebFacel.Controllers
{
    public class FilesUploadedController : Controller
    {
        // GET: FilesUploaded
        public ActionResult Index()
        {
            string newColumn = "Command";
            DataTable dtListFiles = this.ListFiles();
           

            #region Command

            if (dtListFiles.Rows.Count > 0)
            {
                if (!dtListFiles.Columns.Contains(newColumn))
                {
                    dtListFiles.Columns.Add("Command");
                }

                foreach (DataRow dr in dtListFiles.Rows)
                {
                    dr["Command"] = MvcApplication.plantilla_col_edit.Replace("[id]", dr["FileDetailId"].ToString());
                }
            }

            #endregion

            ViewBag.DataModel = dtListFiles.ToJson();

            return View();
        }

        public ActionResult Edit() {

            #region Variables de trabajo

            string errorMessage = string.Empty;
            FilesDetailModel model = new FilesDetailModel();

            #endregion

            ViewBag.Model = JsonConvert.SerializeObject(model);
            ViewBag.AppUrl = MvcApplication.AppUrl;
            ViewBag.Error = errorMessage;

            return View();
        }

        private DataTable ListFiles() {
            DataTable dtList = new DataTable("dtFiles");

            dtList.Columns.Add("FileDetailId");
            dtList.Columns.Add("fileName");

            dtList.Rows.Add("909", "myFilename.xml");
            dtList.Rows.Add("810", "FC_20198.xml");

            return dtList;
        }
    }
}