using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Vector.Common.Entities
{
    public class ErrorManager
    {
        public Error GetGeneralBadRequest()
        {
            Error objEr = new Error();
            objEr.ErrorCode = "400 Input format exception";
            objEr.ErrorDescription = "Invalid input format";
            return objEr;
        }
        public Error GetCustomBadRequest(string message)
        {
            Error objEr = new Error();
            objEr.ErrorCode = "400 Input format exception";
            objEr.ErrorDescription = message;
            return objEr;
        }

        public Error GetGeneralInternalServerError()
        {
            Error objEr = new Error();
            objEr.ErrorCode = "500 Server exception";
            objEr.ErrorDescription = "General Server Exception occurred, contact Administrator";
            return objEr;
        }

        public Error SetErrorResponseWithNoSpecialCharsAllowed()
        {
            Error objEr = new Error();
            objEr.ErrorCode = "400 Bad Request";
            objEr.ErrorDescription = "Special Characters Not Allowed";
            return objEr;
        }

        public Error SetErrorResponseWithNoResourceFound()
        {
            Error objEr = new Error();
            objEr.ErrorCode = "400 Bad Request";
            objEr.ErrorDescription = "No Resource Found";
            return objEr;
        }
    }

    public class Error
    {
        [DataMember(Order = 1)]
        public string ErrorCode { get; set; }
        [DataMember(Order = 2)]
        public string ErrorDescription { get; set; }
    }
}
