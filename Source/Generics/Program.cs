using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello generics!");

            MyGenericClass<int> instance1 = new MyGenericClass<int>(7);

            Console.WriteLine($"{instance1.Value.GetType().Name} - {instance1.Value}");

            MyGenericClass<string> instance2 = new MyGenericClass<string>("seven");

            Console.WriteLine($"{instance2.Value.GetType().Name} - {instance2.Value}");
            
            MyClassBase baseInstance1 = new MyGenericClass<int>(10);

            Console.WriteLine($"{baseInstance1.Value.GetType().Name} - {baseInstance1.Value}");

            MyClassBase baseInstance2 = new MyGenericClass<string>("ten");

            Console.WriteLine($"{baseInstance2.Value.GetType().Name} - {baseInstance2.Value}");

            Console.WriteLine($"{baseInstance2.Value.GetType().Name} - {((MyGenericClass<string>)baseInstance2).Value}");


            var tp = typeof(MyGenericClass<>);
            Type constructedClass = tp.MakeGenericType(typeof(int));
            object created = Activator.CreateInstance(constructedClass);
            Console.ReadLine();
        }
    }

  
}
