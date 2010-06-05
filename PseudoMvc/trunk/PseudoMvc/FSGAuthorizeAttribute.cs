using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinSys.Framework.MasterApp;

namespace PseudoMvc {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FSGAuthorizeAttribute : Attribute {

        private string _systemId, _classId, _objectId;

        public FSGAuthorizeAttribute(string classId, string objectId) {
            _classId = classId;
            _objectId = objectId;
        }

        public FSGAuthorizeAttribute(string systemId, string classId, string objectId) {
            _systemId = systemId;
            _classId = classId;
            _objectId = objectId;
        }

        public void setPageIdentification(FrameworkPage page) {

            page.SetPageIdentity(_systemId, _classId, _objectId);

        }

    }

}
