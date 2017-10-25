using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{

    [DataContract]
    public class Msg
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [ServiceContract(CallbackContract = typeof(ISimpleServiceCallBack))]
    public interface ISimpleService
    {
        [OperationContract(IsOneWay = false)]
        void Subscribe(Guid clientId);
        [OperationContract(IsOneWay = false)]
        void Unsubscribe(Guid clientId);

        //[OperationContract(IsOneWay = true)]
        //void Publish(Msg msg);
    }
    [ServiceContract]
    public interface ISimpleServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void DoWork(Msg msg);
    }
}
