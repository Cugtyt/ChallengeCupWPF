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
        //public static List<Type> GetClassList(string namespace_, string classPartName = null)
        //{
        //    Assembly asm = Assembly.GetExecutingAssembly();
        //    List<Type> classList = new List<Type>();
        //    foreach (var type in asm.GetTypes())
        //    {
        //        if (type.Namespace == namespace_)
        //        {
        //            if (classPartName == null)
        //            {
        //                classList.Add(type);
        //            }
        //            else if(type.Name.Contains(classPartName))
        //            {
        //                classList.Add(type);
        //            }
        //        }
        //    }
        //    return classList;
        //}

        public static List<Type> GetClassList(string namespace_, Type type = null)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            List<Type> classList = new List<Type>();
            foreach (var t in asm.GetTypes())
            {
                if (t.Namespace == namespace_)
                {
                    if (type == null)
                    {
                        classList.Add(t);
                    }
                    else if (t.GetInterface(type.Name) != null)
                    {
                        classList.Add(t);
                    }
                }
            }
            return classList;
        }

    }
}
