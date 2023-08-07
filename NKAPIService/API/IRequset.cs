namespace NKAPIService.API
{
    public interface IRequset
    {
        RequestType RequsetType { get; }
        string GetResource();
    }
}
