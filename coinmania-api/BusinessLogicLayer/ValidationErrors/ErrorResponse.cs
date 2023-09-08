namespace BusinessLogicLayer.ValidationErrors;

public class ErrorResponse
{
    public IEnumerable<ErrorMessage> Errors { get; set; } = new List<ErrorMessage>();
}