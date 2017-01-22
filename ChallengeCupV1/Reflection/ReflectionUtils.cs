using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.Reflection
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Get classes list in given namespace and filtered by given interface
        /// </summary>
        /// <param name="namespace_">namespace to search</param>
        /// <param name="type">filter interface type</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassList(string namespace_, Type type = null)
        {
            //Assembly asm = Assembly.GetExecutingAssembly();
            return from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.Namespace == namespace_
                    where type != null ? t.GetInterface(type.Name) != null : true
                    select t;
                

            //List<Type> classList = new List<Type>();
            //foreach (var t in asm.GetTypes())
            //{
            //    if (t.Namespace == namespace_)
            //    {
            //        if (type == null)
            //        {
            //            classList.Add(t);
            //        }
            //        else if (t.GetInterface(type.Name) != null)
            //        {
            //            classList.Add(t);
            //        }
            //    }
            //}
            //return classList;
        }

    }
}
