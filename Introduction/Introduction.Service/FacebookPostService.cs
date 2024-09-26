using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class FacebookPostService : IFacebookPostService
    {
        private IFacebookPostRepository _repository;
        public FacebookPostService(IFacebookPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> PostAsync(FacebookPost facebookPost)
        {
            bool successfulPost = await _repository.PostAsync(facebookPost);
            return successfulPost;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool successfulDelete = await _repository.DeleteAsync(id);
            return successfulDelete;
        }

        public async Task<FacebookPost> GetByIdAsync(Guid id)
        {
            FacebookPost facebookPost = new FacebookPost();
            facebookPost = await _repository.GetByIdAsync(id);
            return facebookPost;
        }

        public async Task<bool> PutAsync(Guid id, FacebookPost facebookPost)
        {
            bool successfulPut = await _repository.PutAsync(id, facebookPost);
            return successfulPut;
        }
    }
}
