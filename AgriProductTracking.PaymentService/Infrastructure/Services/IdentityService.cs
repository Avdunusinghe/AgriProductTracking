
using System.Security.Claims;

namespace AgriProductTracking.PaymentService.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public string GetUserIdentity()
        {
            var value = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return value;
        }

        public string GetUserName()
        {
            var identity = _context.HttpContext.User.Identity as ClaimsIdentity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var username = claim != null ? claim.Value : string.Empty;

            return username;
        }
    }
}
