namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*Created by Prabu Elavarasan*/
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
