using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using Service;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(SimpleService)))
            {
                host.Open();
                Console.WriteLine("Simple service started....Press any key to start publishing message");
                Console.ReadKey();
                int c = 0;
                while (true)
                {
                    Console.WriteLine($"Time:{DateTime.Now}, Published:{c * 100} Messages");
                    c++;
                    for (int i = 0; i < 100; i++)
                    {
                        Console.Write(i);
                        SimpleService.Publish(new Msg { Id = i, Name = $"Name{DateTime.Now}" });
                    }
                    System.Threading.Thread.Sleep(300);
                }
            }
        }
    }
}
