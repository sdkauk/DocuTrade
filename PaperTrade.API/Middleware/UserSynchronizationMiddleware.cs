using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Claims;
using PaperTrade.BusinessLogic.Services.Users.Requests;

namespace PaperTrade.API.Middleware;

public class UserSynchronizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserSynchronizationMiddleware> _logger;

    public UserSynchronizationMiddleware(RequestDelegate next, ILogger<UserSynchronizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService, IClaimsService claimsService)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var objectId = claimsService.GetObjectId(context.User);

            if (!string.IsNullOrEmpty(objectId))
            {
                try
                {
                    var user = await userService.GetCurrentUserAsync(objectId);

                    if (user == null)
                    {
                        _logger.LogInformation("Creating new user for Object ID: {ObjectId}", objectId);

                        var request = new UserPostRequest
                        {
                            ObjectIdentifier = objectId,
                            Email = claimsService.GetEmail(context.User) ?? "no-email@example.com",
                            DisplayName = claimsService.GetDisplayName(context.User) ?? "Unknown User"
                        };

                        await userService.CreateUserAsync(request);
                        _logger.LogInformation("User created successfully for Object ID: {ObjectId}", objectId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in user synchronization for Object ID: {ObjectId}", objectId);
                }
            }
            else
            {
                _logger.LogWarning("Authenticated user has no Object ID claim");
            }
        }

        await _next(context);
    }
}