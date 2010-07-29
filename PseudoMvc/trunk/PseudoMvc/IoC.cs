using System;
using System.Collections.Generic;
using System.Linq;

namespace PseudoMvc {
    public class IoC {
        private static Dictionary<Type, Func<object>> RegisteredTypes { get; set; }


        static IoC() {
            RegisteredTypes = new Dictionary<Type, Func<object>>();
        }

        public static void Register(Type T, Type TConcrete) {

            if (RegisteredTypes.ContainsKey(T)) {
                throw new DuplicateItemFoundException(String.Format("Cannot register type: {0} as one is already registered.", T.Name));
            }

            RegisteredTypes.Add(T, () => { return Activator.CreateInstance(TConcrete); });

        }

        /// <summary>
        /// Registers a concrete type
        /// Will reregister the concrete class to the generic type if already registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void Register<TConcrete>() where TConcrete : class, new() {
            
            Register(typeof(TConcrete), typeof(TConcrete));
            
        }

        /// <summary>
        /// Registers a concrete type to 
        /// Will reregister the concrete class to the generic type if already registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void Register<T, TConcrete>() where TConcrete : class, new() {

            Register(typeof(T), typeof(TConcrete));

        }

        public static T Resolve<T>() {

            if (!RegisteredTypes.ContainsKey(typeof(T))) {
                throw new KeyNotFoundException(String.Format("Type: {0} is not registered and cannot be resolved.", typeof(T).Name));
            }

            var function = RegisteredTypes[typeof(T)];
            return (T)function.Invoke();

        }

        public static object ResolveFromName(string name) {

            var function = RegisteredTypes.Where(t => t.Key.Name.ToLower() == name.ToLower()).FirstOrDefault().Value;

            if (function == null) {
                throw new KeyNotFoundException(String.Format("Type: {0} is not registered and cannot be resolved.", name));
            }

            return function.Invoke();

        }

        public static void CleanupTypes() {

            RegisteredTypes.Clear();

        }

    }
}
