
using System;
using System.Collections.Generic;

namespace community.ListUtils {
    class ListConverter {
        public static List<U> Map<T,U>(List<T> inList,Func<T,U> lambda) {
            List<U> result = new List<U>();
            foreach(T elm in inList) {
                result.Add(lambda(elm));
            }
            return result;
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