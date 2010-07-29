using System;
using System.Web.Configuration;

namespace PseudoMvc {

    public class FrameworkPage : FinSys.Framework.MasterApp.FrameworkBasePage, IRoutablePage {

        public System.Web.Routing.RequestContext RequestContext { get; set; }

        public bool IsDebugMode {
            get {
                if (WebConfigurationManager.AppSettings["IsDebug"] != null)
                    return Convert.ToBoolean(WebConfigurationManager.AppSettings["ISDebug"]);
                else
                    return false;
            }
        }

        /// <summary>
        /// Uses AppSetting SystemId for the systemId when tied into FSG Security.
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="objectid"></param>
        public void SetPageIdentity(String systemId, string classId, string objectId) {
            var systemid = systemId ?? System.Web.Configuration.WebConfigurationManager.AppSettings["SystemId"];
            
            if (!IsDebugMode) { // If is NOT in Debug mode
                // Call method on the FrameworkBasePage
                try {
                    this.SetPageIdentification(systemid, classId, objectId);
                }
                catch (System.Web.HttpException ex) {
                    if (ex.Message.ToLower().Contains("response is not available"))
                        System.Web.HttpContext.Current.Response.Redirect(System.Web.VirtualPathUtility.ToAbsolute("/login/login.aspx"));
                    else
                        throw ex;
                }
                
            }
            else {
                this.SystemId = systemid;
                this.ClassId = classId;
                this.ObjectId = objectId;
            }
        }

    }

}
