using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Net.Http;

namespace ProcessClaimService
{
    //A global exception handler that will be used to catch any error
    public class ClaimExceptionHandler : ExceptionHandler
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Exceptions");
        private class ErrorInformation
        {
            public string Message { get; set; }
            public DateTime ErrorDate { get; set; }
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception != null)
            log.Error(context.Exception.Message +"Stack Trace : "+ context.Exception.StackTrace);
            //Return a DTO representing what happened
            context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError,
              new ErrorInformation { Message = "We apologize but an unexpected error occured. Please try again later.", ErrorDate = DateTime.UtcNow }));
  
        }
    }
}