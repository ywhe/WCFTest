using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SimpleServiceCallBack : ISimpleServiceCallBack
    {
        public void DoWork(Msg msg)
        {
            Console.WriteLine($"Thread:[{System.Threading.Thread.CurrentThread.ManagedThreadId}] starts to process message at:{DateTime.Now}");
            System.Threading.Thread.Sleep(1000*10);
            Console.WriteLine($"Message:ID:{msg.Id}  Name:{msg.Name}");
        }
    }
}
