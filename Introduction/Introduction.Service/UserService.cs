using Introduction.Common;
using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using Npgsql;

namespace Introduction.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> PostAsync(User user)
        {
            bool successfulPost = await _repository.PostAsync(user);
            return successfulPost;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool successfulDelete = await _repository.DeleteAsync(id);
            return successfulDelete;
        }

        public async Task<User> GetAsync(Guid id)
        {
            User user = new User();
            user = await _repository.GetAsync(id);
            return user;
        }

        public async Task<List<FacebookPost>> GetUserAndPostsAsync(Guid id)
        {
            List<FacebookPost> posts = new List<FacebookPost>();
            posts = await _repository.GetUserAndPostsAsync(id);
            return posts;
        }

        public async Task<bool> PutAsync(Guid id, User user)
        {
            bool successfulPut = await _repository.PutAsync(id, user);
            return successfulPut;
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            users = await _repository.GetAllAsync();
            return users;
        }
    }
}
