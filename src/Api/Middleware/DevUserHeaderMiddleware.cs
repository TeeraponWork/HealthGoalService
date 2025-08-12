using Microsoft.Extensions.Options;

namespace Api.Middleware
{
    public sealed class DevUserHeaderOptions
    {
        public bool Enabled { get; set; } = true;
        public Guid UserId { get; set; } = Guid.Parse("8a9defb6-e562-405b-9ac1-672e238bd20f");
        public string? Email { get; set; } = "dev@local";
        public string[]? Roles { get; set; } = new[] { "User" };
    }

    public sealed class DevUserHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DevUserHeaderOptions _opt;

        public DevUserHeaderMiddleware(RequestDelegate next, IOptions<DevUserHeaderOptions> opt)
        {
            _next = next;
            _opt = opt.Value;
        }

        public async Task Invoke(HttpContext ctx)
        {
            if (_opt.Enabled)
            {
                var headers = ctx.Request.Headers;
                if (string.IsNullOrWhiteSpace(headers["X-User-Id"]))
                    headers["X-User-Id"] = _opt.UserId.ToString();

                if (!string.IsNullOrWhiteSpace(_opt.Email) && string.IsNullOrWhiteSpace(headers["X-Email"]))
                    headers["X-Email"] = _opt.Email;

                if (_opt.Roles is { Length: > 0 } && string.IsNullOrWhiteSpace(headers["X-Roles"]))
                    headers["X-Roles"] = string.Join(",", _opt.Roles);
            }

            await _next(ctx);
        }
    }
}
