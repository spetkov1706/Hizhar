namespace Hizhar.Services.Models
{
    public class OperationResult
    {
        private OperationResult(bool succeeded, string? errorMessage)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public bool Succeeded { get; }

        public string? ErrorMessage { get; }

        public static OperationResult Success() => new OperationResult(true, null);

        public static OperationResult Failure(string errorMessage) => new OperationResult(false, errorMessage);
    }
}
