namespace BackCore.Utilities.ProcessingResult
{
    public static class ProcessResultMapping
    {
        public static ProcessResult<T2> Map<T1, T2>(ProcessResult<T1> input, ProcessResult<T2> output)
        {
            output.Exception = input.Exception;
            output.Message = input.Message;
            output.MethodName = input.MethodName;
            output.IsSucceeded = input.IsSucceeded;
            output.Status = input.Status;
            return output;
        }
    }
}
