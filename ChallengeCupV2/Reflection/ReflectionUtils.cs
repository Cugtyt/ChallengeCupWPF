using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.Reflection
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Get classes list in given namespace
        /// </summary>
        /// <param name="namespace_">namespace to search</param>
        /// <param name="type">filter interface type</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassList(string namespace_)
        {
            if (namespace_ == null)
            {
#if DEBUG
                Console.WriteLine("ReflectionUtils: GetClassList() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("ReflectionUtils: GetClassList()");
            }
            //Assembly asm = Assembly.GetExecutingAssembly();
            return from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.Namespace == namespace_
                    //where type != null ? t.GetInterface(type.Name) != null : true
                    select t;
        }

        /// <summary>
        /// Keep elements are interface of the input type in rowType
        /// </summary>
        /// <param name="rowType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> IsInterfaceFilter(IEnumerable<Type> rowType, Type type)
        {
            if (rowType == null || type == null)
            {
#if DEBUG
                Console.WriteLine("ReflectionUtils: IsInterfaceFilter() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("ReflectionUtils: IsInterfaceFilter()");
            }
            return from t in rowType
                   where t.GetInterface(type.Name) != null
                   select t;
        }

        /// <summary>
        /// Remove elements are not interface of the input type in rowType
        /// </summary>
        /// <param name="rowType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> IsNotInterfaceFilter(IEnumerable<Type> rowType, Type type)
        {
            if (rowType == null || type == null)
            {
#if DEBUG
                Console.WriteLine("ReflectionUtils: IsNotInterfaceFilter() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("ReflectionUtils: IsNotInterfaceFilter()");
            }
            return from t in rowType
                   where t.GetInterface(type.Name) == null
                   select t;
        }

        /// <summary>
        /// Get an instance of a class by given class name
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object GetClassByName(string className)
        {
            if (className == null)
            {
#if DEBUG
                Console.WriteLine("ReflectionUtils: GetClassByName() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("ReflectionUtils: GetClassByName()");
            }
            return Assembly.GetExecutingAssembly().CreateInstance(className);
        }
    }
}
