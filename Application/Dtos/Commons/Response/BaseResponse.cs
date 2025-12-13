namespace Application.Dtos.Commons.Response;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int? TotalRecords { get; set; }
}
