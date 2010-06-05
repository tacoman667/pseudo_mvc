using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PseudoMvc {
    public static class ExtensionMethods {

        public static object SetPropertyValue(this object obj, string propertyName, object value) {

            try {
                var propertyInfo = obj.GetType().GetProperty(propertyName);
                var type = propertyInfo.PropertyType;
                object val = value;
                

                if (val != null) {
                    if (type.IsGenericType && type.Name.ToLower().Contains("nullable"))
                        val = Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
                    else
                        val = Convert.ChangeType(value, type);
                }

                propertyInfo.SetValue(obj, val, null);

            }
            catch (Exception ex) {
                throw ex;
            }

            return obj;

        }

        public static object GetPropertyValue(this object obj, string propertyName) {

            try {
                var val = obj.GetType().GetProperty(propertyName).GetValue(obj, null);
                return val;
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public static IEnumerable<System.Reflection.MethodInfo> GetAllMethods(this object obj) {

            try {
                return obj.GetType().GetMethods();
            }
            catch (Exception ex) {
                
                throw ex;
            }

        }

        public static object CallMethod(this object obj, string methodName, params object[] args) {

            try {
                var method = obj.GetType().GetMethod(methodName);
                if (method != null)
                    return method.Invoke(obj, args);
            }
            catch (Exception ex) {
                throw ex;
            }

            return null;

        }

        public static object ChangeTypeTo(this object obj, Type type) {

            try {
                return Convert.ChangeType(obj, type);
            }
            catch (Exception ex) {
                
                throw ex;
            }

        }

        /// <summary>
        /// Changes From relative url string to absolute url string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUrl(this string str) {
            return System.Web.VirtualPathUtility.ToAbsolute("~" + str);
        }

        /// <summary>
        /// Changes From relative url string to absolute url string and wraps in quotes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUrl(this string str, bool withQuotes) {
            string url = System.Web.VirtualPathUtility.ToAbsolute("~" + str);
            if (withQuotes)
                url = url.Insert(0, "\"").Insert(url.Length + 1, "\"");
            return url;
        }

        /// <summary>
        /// Executes a 'for' loop and executes the lambda function.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="action"></param>
        public static void Times(this int x, Action action) {

            for (int i = 0; i < x; i++) {
                action.Invoke();
            }

        }

        /// <summary>
        /// Executes a 'for' loop and executes the lambda function taking the current iterator as an argument.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="action"></param>
        public static void Times(this int x, Action<int> action) {

            for (int i = 0; i < x; i++) {
                action.Invoke(i);
            }

        }

    }
}
