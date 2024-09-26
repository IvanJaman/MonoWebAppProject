using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IFacebookPostService
    {
        public Task<bool> PostAsync(FacebookPost facebookPost);
        public Task<bool> DeleteAsync(Guid id);
        public Task<FacebookPost> GetByIdAsync(Guid id);
        public Task<bool> PutAsync(Guid id, FacebookPost facebookPost);
    }
}
