using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public class MyGenericClass<T> : MyClassBase
    {
        public MyGenericClass()
        {
         
        }


        public MyGenericClass(T someType)
        {
            this.m_SomeType = someType;
        }

        public new T Value { get { return (T)this.m_SomeType; } }
    }

    public class MyClassBase
    {
        protected object m_SomeType;

        public object Value { get { return this.m_SomeType; } }
    }
}
