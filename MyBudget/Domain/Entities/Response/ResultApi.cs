namespace Domain.Entities.Response;
public class ResultApi
{

    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }

    public ResultApi(bool success,string? message, object? data)
    {
        this.Data = data;
        this.Success = success;
        this.Message = message;
    }

    public static ResultApi Error(string message) => new(false, message, null);

    public static ResultApi Error(Exception e) => new(false, e.Message, null);

    public static ResultApi Execute(object data) => new(true, null, data);
}