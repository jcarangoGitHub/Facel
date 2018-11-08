
#region Referencias

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

#endregion

namespace DBE.Net.Facel.WebFacel
{
    public class MvcApplication : System.Web.HttpApplication
    {

        #region Miembros estaticos

        private static int horasCache = 12;
        public static string AppUrl = string.Empty;
        public static string DATA_PATH = string.Empty;
        public static string APP_PATH = string.Empty;
        public static string plantilla_col_edit = @"<button type=""button"" class=""btn btn-success"" onclick=""edit([id])""><i class=""fa fa-edit""></i></button>";
        #endregion

        #region Application_Start

        protected void Application_Start()
        {

            #region Inicializacion predeterminada

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #endregion

            #region Cultura en-US

            string lang = "en-US";
            CultureInfo cultureToUse = new CultureInfo(lang);

            cultureToUse.NumberFormat.NumberGroupSeparator = ",";
            cultureToUse.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = cultureToUse;
            Thread.CurrentThread.CurrentUICulture = cultureToUse;

            #endregion

            #region Ruta datos

            APP_PATH = HttpContext.Current.Server.MapPath("~");
            DATA_PATH = string.Format(@"{0}\App_Data\", APP_PATH);

            AppUrl = System.Web.HttpRuntime.AppDomainAppVirtualPath;

            if (AppUrl.Equals("/"))
            {
                AppUrl = string.Empty;
            }

            #endregion

        }

        #endregion
    }
}
