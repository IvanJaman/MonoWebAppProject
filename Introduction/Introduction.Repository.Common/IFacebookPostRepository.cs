using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IFacebookPostRepository
    {
        public Task<bool> PostAsync(FacebookPost facebookPost);
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> PutAsync(Guid id, FacebookPost facebookPost);
        public Task<FacebookPost> GetByIdAsync(Guid id);
    }
}
