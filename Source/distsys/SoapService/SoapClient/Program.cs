using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.HelloWorldServiceClient client = new ServiceReference1.HelloWorldServiceClient();
            var result = client.SayHello("Hello it is me :)");
        }
    }
}
