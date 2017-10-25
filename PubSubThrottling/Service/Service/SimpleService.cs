using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SimpleService : ISimpleService
    {

        static Dictionary<Guid, ISimpleServiceCallBack> clients = new Dictionary<Guid, ISimpleServiceCallBack>();

        public static void Publish(Msg msg)
        {
            //you can also use event to maintain the clients...
            try
            {
                clients.Values.AsParallel().ForAll(client =>
                {
                    try
                    {
                        client.DoWork(msg);
                    }
                    catch (Exception)
                    {
                        //TODO...
                    }
                }
           );
            }
            catch (Exception)
            {

            }
        }

        public void Subscribe(Guid clientId)
        {
            if (clients.ContainsKey(clientId)) { throw new Exception($"Client with Id[{clientId}] exists...."); }
            var callback = OperationContext.Current.GetCallbackChannel<ISimpleServiceCallBack>();
            clients[clientId] = callback;
        }

        public void Unsubscribe(Guid clientId)
        {
            //TODO  not sure if we need to dispose the callback channel manually....
            if (clients.ContainsKey(clientId))
            {
                clients.Remove(clientId);
            }
        }

    }
}
