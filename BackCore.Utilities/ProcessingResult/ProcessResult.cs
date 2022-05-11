using System;
using System.Net;

namespace BackCore.Utilities.ProcessingResult
{
    public class ProcessResult<T>
    {
        public string Message;
        public string MethodName;

        public ProcessResult(string methodName)
        {
            this.MethodName = methodName;
        }

        public ProcessResult()
        {
        }

        public bool IsSucceeded { get; set; }

        public HttpStatusCode Status { get; set; }

        public Exception Exception { get; set; }

        public T returnData { get; set; }

        public string Name { get; set; }
    }
}
