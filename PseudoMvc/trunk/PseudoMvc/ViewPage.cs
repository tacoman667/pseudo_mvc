using System;


namespace PseudoMvc {

    public abstract class ViewPage : ViewPage<object> {

    }

    public abstract class ViewPage<TModel> : FrameworkPage where TModel : class, new() {
        
        public ViewData<TModel> ViewData { get; set; }
        public HtmlHelper<TModel> Html { get; set; }


        public TModel Model { 
            get { return this.ViewData.Model; }
            set { this.ViewData.Model = value; }
        }

        public ViewPage() {
            this.ViewData = new ViewData<TModel>();
            this.Html = new HtmlHelper<TModel>(this.Model);
        }

        public void Init() {
            this.Html = new HtmlHelper<TModel>(this.Model);
        }

        protected void Page_PreInit(object sender, EventArgs e) {
            if (base.IsDebugMode)
                this.MasterPageFile = "~/Views/Shared/Site.master";
        }

    }
}
