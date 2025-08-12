namespace Api.Security
{
    public interface IUserContext
    {
        Guid UserId { get; }
        string? Email { get; }
    }

    public sealed class UserContext(IHttpContextAccessor accessor) : IUserContext
    {
        public Guid UserId
        {
            get
            {
                var id = accessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault();
                return Guid.TryParse(id, out var g) ? g : throw new InvalidOperationException("X-User-Id header missing/invalid");
            }
        }
        public string? Email => accessor.HttpContext?.Request.Headers["X-Email"].FirstOrDefault();
    }
}
