using ContactList.Application.Contracts;

namespace ContactList.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public Guid? UserId => new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue("sub"));
        public Guid? UserId => Guid.NewGuid();

    }
}
