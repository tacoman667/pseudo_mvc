using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Linq.Expressions;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PseudoMvc {
    public class HtmlHelper<TModel> : IDisposable where TModel : class, new() {

        private TModel Model { get; set; }

        public HtmlHelper(TModel model) {
            Model = model;
        }

        #region ActionLink Helpers

        public string ActionLink(string url, string linkText) {
            return ActionLink(url, linkText, null);
        }

        public string ActionLink(string url, string linkText, object attributes) {

            var actionlink = String.Empty;

            if (url.Contains("~/"))
                url = VirtualPathUtility.ToAbsolute(url);

            actionlink += String.Format("<a href='{0}' ", url);
            actionlink += AddAttributes(attributes);
            actionlink += ">";
            actionlink += linkText;
            actionlink += "</a>";

            return actionlink;

        }

        #endregion

        #region Button Helpers

        public string SubmitButton(string id, string text) {
            return SubmitButton(id, text, null);
        }

        public string SubmitButton(string id, string text, object attributes) {

            var submitButton = string.Empty;

            submitButton += "<input type='submit' ";
            submitButton += AddIdForControl(id);
            submitButton += String.Format(" value='{0}' ", text);
            submitButton += AddAttributes(attributes);
            submitButton += " />";

            return submitButton;

        }

        #endregion

        #region CheckBox Helpers

        public string CheckBox(string id, object displayText, bool isSelected) {
            return CheckBox(id, displayText, isSelected, null);
        }

        public string CheckBox(string id, object displayText, bool isSelected, object attributes) {

            var checkBox = String.Empty;

            checkBox += "<input type='checkbox' ";
            checkBox += AddIdForControl("_" + id);
            if (isSelected)
                checkBox += " checked ";
            checkBox += AddAttributes(attributes);
            checkBox += String.Format(" /><label for='{0}'>{1}</label>", "_" + id, displayText);

            checkBox += "<input type='hidden' ";
            checkBox += AddIdForControl(id);
            checkBox += String.Format(" value='{0}' />", isSelected);

            checkBox += "<script type='text/javascript'>";
            checkBox += String.Format("$(\"input[id$={0}]\").change(function()", String.Concat("_", id));
            checkBox += "{var val = false;";
            checkBox += "if($(this).attr(\"checked\")){val = true;}";
            checkBox += String.Format("$(\"input[id$={0}]\").val(val);", id);
            checkBox += "});";
            checkBox += "</script>";

            return checkBox;

        }

        #endregion

        #region DropDownList Helpers

        public string DropDownList(string id, IEnumerable values) {
            return DropDownList(id, values, "", "", null);
        }

        public string DropDownList(string id, IEnumerable values, string DataTextField, string DataValueField) {
            return DropDownList(id, values, DataTextField, DataValueField, null, null);
        }

        public string DropDownList(string id, IEnumerable values, string DataTextField, string DataValueField, object SelectedValue) {
            return DropDownList(id, values, DataTextField, DataValueField, SelectedValue, null);
        }

        public string DropDownList(string id, IEnumerable values, string DataTextField, string DataValueField, object SelectedValue, object attributes) {

            var dropdownlist = "<select ";
            var items = new SelectList(values, DataValueField, DataTextField, SelectedValue);

            dropdownlist += AddIdForControl(id);
            dropdownlist += AddAttributes(attributes);

            dropdownlist += ">";

            foreach (var item in items) {

                dropdownlist += String.Format("<option value='{0}'", item.Value);
                if (item.Selected)
                    dropdownlist += String.Format(" selected='selected' ");
                dropdownlist += String.Format(">{0}</option>", item.Text);

            }

            dropdownlist += " ></select>";

            return dropdownlist;
        }

        public string DropDownListFor(Expression<Func<TModel, string>> action, IEnumerable list, string dataTextField, string dataValueField, object selectedValue) {
            return DropDownListFor(action,
                                   list,
                                   dataTextField,
                                   dataValueField,
                                   selectedValue,
                                   null);
        }

        public string DropDownListFor(Expression<Func<TModel, string>> action, IEnumerable list, string dataTextField, string dataValueField, object selectedValue, object attributes) {
            var expression = (MemberExpression)action.Body;
            var function = action.Compile();
            string value = function(this.Model);
            return DropDownList(expression.ToString(),
                                list,
                                dataTextField,
                                dataValueField,
                                selectedValue,
                                attributes);
        }

        #endregion

        #region Form Helpers

        public string Form<T>(Expression<Func<T, ActionResult>> action, FormActions verb) {

            var expression = (MethodCallExpression)action.Body;
            var function = action.Compile();
            //string value = function(this.Model);

            var form = new StringBuilder();
            var url = String.Format("~/{0}/{1}", expression.Method.ReflectedType.Name.Replace("Controller", ""), expression.Method.Name);
            
            form.AppendFormat("<form action='{0}' method='{1}'>", VirtualPathUtility.ToAbsolute(url), verb.ToString());


            return form.ToString();

        }

        public string BeginForm(string controller, string action) {
            return BeginForm(controller, action, FormActions.POST);
        }

        public string BeginForm(string action, string controller, FormActions verb) {
            var form = new StringBuilder();
            var url = String.Format("~/{0}/{1}", controller, action);
            form.AppendFormat("<form action='{0}' method='{1}'>", VirtualPathUtility.ToAbsolute(url), verb.ToString());
            
            return form.ToString();
        }

        public string EndForm() {
            return "</form>";
        }

        #endregion

        #region Hidden Input Helpers

        public string HiddenInput(string id, object value) {

            var hiddeninput = String.Empty;

            hiddeninput += "<input type='hidden' ";
            hiddeninput += String.Format("id='{0} name='{0}' ", id);
            hiddeninput += String.Format("value='{0}' ", value);
            hiddeninput += " />";

            return hiddeninput;
        }

        #endregion

        #region RadioButton Helpers

        public string RadioButton(string id, object displayText, bool isSelected) {
            return RadioButton(id, displayText, isSelected, null);
        }

        public string RadioButton(string id, object displayText, bool isSelected, object attributes) {

            var radiobutton = String.Empty;

            radiobutton += "<input type='radio' ";
            radiobutton += AddIdForControl(id);
            radiobutton += String.Format("value='{0}'", displayText);
            if (isSelected)
                radiobutton += " checked ";
            radiobutton += AddAttributes(attributes);
            radiobutton += String.Format(" >{0}</input>", displayText);

            return radiobutton;

        }

        public string RadioButtonFor(Expression<Func<TModel, object>> action, object displaytext, bool isSelected) {
            var expression = (UnaryExpression)action.Body;
            return RadioButton(expression.Operand.ToString(), displaytext, isSelected);
        }

        #endregion

        #region TextArea

        public string TextArea(string id) {

            return TextArea(id, String.Empty, null);

        }

        public string TextArea(string id, string value) {

            return TextArea(id, value, null);

        }

        public string TextArea(string id, string value, object properties) {

            var textarea = String.Format("<textarea {0} ", AddIdForControl(id));

            textarea += AddAttributes(properties);

            textarea += String.Format(" >{0}</textarea>", value);

            return textarea;

        }

        public string TextAreaFor(Expression<Func<TModel, string>> action) {
            return TextAreaFor(action, null);
        }

        public string TextAreaFor(Expression<Func<TModel, string>> action, object attributes) {
            var expression = (MemberExpression)action.Body;
            var function = action.Compile();
            string value = function(this.Model);
            return TextArea(expression.ToString(), value, attributes);
        }

        #endregion

        #region TextBox Helpers

        public string TextBox(string id) {

            return TextBox(id, String.Empty, null);

        }

        public string TextBox(string id, string value) {

            return TextBox(id, value, null);

        }

        public string TextBox(string id, string value, object properties) {

            var textbox = String.Format("<input {0} value='{1}' type='text' ", AddIdForControl(id), value);

            textbox += AddAttributes(properties);

            textbox += " />";

            return textbox;

        }

        public string TextBoxFor(Expression<Func<TModel, string>> action) {
            return TextBoxFor(action, null);
        }

        public string TextBoxFor(Expression<Func<TModel, string>> action, object attributes) {
            var expression = (MemberExpression)action.Body;
            var function = action.Compile();
            string value = function(this.Model);
            return TextBox(expression.ToString(), value, attributes);
        }

        #endregion

        #region ValidationMessage Helpers

        public string ValidationMessagefor(Expression<Func<TModel, string>> action) {
            return ValidationMessagefor(action, null);
        }

        public string ValidationMessagefor(Expression<Func<TModel, string>> action, object attributes) {
            if (HttpContext.Current.Request.RequestType == "GET") return String.Empty;


            var expression = (MemberExpression)action.Body;
            var function = action.Compile();
            string value = function(this.Model);

            var validationMessage = new StringBuilder();

            var atts = expression.Member.GetCustomAttributes(true);


            foreach (var a in atts) {
                if (a is ValidationAttribute) {
                    var att = a as ValidationAttribute;
                    if (!att.IsValid(value)) {
                        return String.Format("<span style='color:red;' {1}>{0}</span>", att.ErrorMessage, AddAttributes(attributes));
                    }
                }
            }

            return String.Empty;

        }

        #endregion

        private string AddIdForControl(string id) {

            var Id = id.Split(".".ToCharArray());
            var formattedId = String.Empty;

            if (Id.Count() == 1)
                formattedId = id;
            else {
                int skip = 0;
                if (Id.Count() > 1) skip = 1;
                formattedId = Id.Skip(skip).Aggregate((a, b) => {
                    if (b != null || b.Length > 0)
                        return String.Format("{0}.{1}", a, b);
                    return a;
                });
            }

            return String.Format(" id='{0}' name='{0}' ", formattedId);

        }

        private string AddAttributes(object properties) {
            var attributes = String.Empty;

            if (properties != null) {
                foreach (var p in properties.GetType().GetProperties()) {

                    attributes += String.Format("{0}", AddAttributeWithValue(p.Name, p.GetValue(properties, null).ToString()));

                }
            }
            return attributes;
        }

        private string AddAttributeWithValue(string property, string value) {

            return String.Format(" {0}='{1}'", property, value);

        }

        /// <summary>
        /// Uses HtmlEncoding to make sure that no rogue code is executed on the page.
        /// </summary>
        /// <param name="valueToEncode"></param>
        /// <returns></returns>
        public string Encode(object valueToEncode) {
            return HttpContext.Current.Server.HtmlEncode(Convert.ToString(valueToEncode));
        }


        public void Dispose() {
            throw new NotImplementedException();
        }
    }

    public enum FormActions {

        GET,
        POST,
        UPDATE,
        DELETE

    }
}
