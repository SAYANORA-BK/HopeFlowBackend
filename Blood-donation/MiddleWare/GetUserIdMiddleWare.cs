using Microsoft.Data.SqlClient;
using System.Security.Claims;

public class GetUserIdMiddleWare
{
    private readonly ILogger<GetUserIdMiddleWare> _logger;
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public GetUserIdMiddleWare(ILogger<GetUserIdMiddleWare> logger, RequestDelegate next, IConfiguration configuration)
    {
        _logger = logger;
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out var userId))
            {
                context.Items["UserId"] = userId;

                try
                {
                    var connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using var connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();

                    var command = new SqlCommand("SELECT role FROM users WHERE id = @userId", connection);
                    command.Parameters.AddWithValue("@userId", userId);

                    var result = await command.ExecuteScalarAsync();

                    if (result != null)
                    {
                        var role = result.ToString()?.ToLower();
                        context.Items["UserRole"] = role;
                        _logger.LogInformation($"UserId {userId} has role: {role}");
                    }
                    else
                    {
                        _logger.LogWarning($"No role found for userId {userId}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching role from users table.");
                }
            }
            else
            {
                _logger.LogWarning("Invalid or missing NameIdentifier claim.");
            }
        }

        await _next(context);
    }
}
