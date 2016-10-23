
using System;
using System.Collections.Generic;

namespace community.ListUtils {
    /**
    *   Diffrent list operations that was helpful to convert and work whit whole group of items
    */
    class ListConverter {
        public static List<U> Map<T,U>(List<T> inList,Func<T,U> lambda) {
            List<U> result = new List<U>();
            foreach(T elm in inList) {
                result.Add(lambda(elm));
            }
            return result;
        }

        public static void DoAction<T>(List<T> inList,Action<T> lambda) {
            foreach(T elm in inList) {
                lambda(elm);  
            }
        }

        public static List<T> Filter<T>(List<T> inList,Func<T,bool> lambda) {
            List<T> result = new List<T>();
            foreach(T elm in inList) {
                if(lambda(elm)) {
                    result.Add(elm);
                }                
            }
            return result;
        }
        public static U Reduce<T,U>(List<T> inList,U initialVal,Func<U,T,U> lambda) {
            U result = initialVal;
            foreach(T elm in inList) {
                result = lambda(result,elm);
            }
            return result;
        }
    }
}