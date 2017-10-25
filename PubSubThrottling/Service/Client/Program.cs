using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            Console.Title = id.ToString();
            InstanceContext site = new InstanceContext(new SimpleServiceCallBack());

            DuplexChannelFactory<ISimpleService> df = new DuplexChannelFactory<ISimpleService>(site, "WSDualHttpBinding_ISimpleService");
            var binding = (df.Endpoint.Binding as WSDualHttpBinding);
            binding.ClientBaseAddress = new Uri($"http://localhost:8377/{id.ToString()}");

            var client = df.CreateChannel();

            client.Subscribe(id);
            Console.WriteLine("Sub....");
            Console.WriteLine("Press Enter to exit....");
            Console.ReadLine();
        }
    }
}
