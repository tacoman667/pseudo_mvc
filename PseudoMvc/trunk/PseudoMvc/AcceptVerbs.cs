using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {

    [global::System.AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpVerb : Attribute {
    }
    
    [global::System.AttributeUsage(AttributeTargets.Method)]
    public class HttpGet : HttpVerb {
    }

    [global::System.AttributeUsage(AttributeTargets.Method)]
    public class HttpPost : HttpVerb {
    }

    [global::System.AttributeUsage(AttributeTargets.Method)]
    public class HttpSet : HttpVerb {
    }

    [global::System.AttributeUsage(AttributeTargets.Method)]
    public class HttpDelete : HttpVerb {
    }
}
