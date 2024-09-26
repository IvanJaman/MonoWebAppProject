using Introduction.Common;
using Introduction.Model;


namespace Introduction.Repository.Common
{
    public interface IUserRepository
    {
        public Task<bool> PostAsync(User user);
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> PutAsync(Guid id, User user);
        public Task<User> GetAsync(Guid id);
        public Task<List<FacebookPost>> GetUserAndPostsAsync(Guid id);
        public Task<List<User>> GetAllAsync();
    } 
}
