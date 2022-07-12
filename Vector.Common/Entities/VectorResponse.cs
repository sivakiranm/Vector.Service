using System;
using System.Runtime.Serialization;
using Vector.Common.BusinessLayer;

namespace Vector.Common.Entities
{
    public class TestData
    {

        public string name { get; set; }
        public string data { get; set; }

        public static implicit operator string(TestData v)
        {
            throw new NotImplementedException();
        }
    }

    public class VectorResponse<T> : DisposeLogic
    {
        [DataMember(Order = 1)]
        public string ResponseType
        {
            get
            {
                return typeof(T).Name;
            }
            set { }
        }

        [DataMember(Order = 2)]
        public string Response { get; set; }    
        

        [DataMember(Order = 3)]
        public string ResponseMessage { get; set; }

        [DataMember(Order = 4)]
        public string ResponseCode { get; set; }

        [DataMember(Order = 3)]
        public T ResponseData { get; set; }

        [DataMember(Order = 6)]
        public Error Error { get; set; }
    }

    public class ResultInfo : DisposeLogic
    {
            public int Result { get; set; }
            public string ResultMessage { get; set; }
    }

}
