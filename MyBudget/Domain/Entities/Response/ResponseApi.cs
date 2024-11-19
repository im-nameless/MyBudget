namespace Domain.Entities.Response;
public class ResponseApi
{
    public ResponseApi(bool success, string? message = null, object? data = null)
    {
        this.Success = success;
        this.Message = message;
        this.Data = data;
    }

    public ResponseApi(Exception e)
    {
        this.Success = false;
        this.Message = $"{e.Message}";
        this.Data = null;
    }

    public bool Success { get; set; }
    public object? Data { get; set; } = default;
    public string? Message { get; set; } = null;
}