using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {
    public class IoC {
        private static Dictionary<Type, Func<object>> RegisteredTypes { get; set; }


        static IoC() {
            RegisteredTypes = new Dictionary<Type, Func<object>>();
        }

        /// <summary>
        /// Registers a concrete type to 
        /// Will reregister the concrete class to the generic type if already registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void Register<T>(Func<object> action) {

            if (RegisteredTypes.ContainsKey(typeof(T))) {
                RegisteredTypes.Remove(typeof(T));
                //throw new DuplicateItemFoundException(String.Format("Cannot register type: {0} as one is already registered.", typeof(T).Name));
            }

            RegisteredTypes.Add(typeof(T), action);

        }

        public static T Resolve<T>() {

            if (!RegisteredTypes.ContainsKey(typeof(T))) {
                throw new KeyNotFoundException(String.Format("Type: {0} is not registered and cannot be resolved.", typeof(T).Name));
            }

            var function = RegisteredTypes[typeof(T)];
            return (T)function.Invoke();

        }

        public static void CleanupTypes() {

            RegisteredTypes.Clear();

        }

    }
}
