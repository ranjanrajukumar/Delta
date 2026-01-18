using Serilog;

namespace Delta.Shared.Logging;

public static class AuditLogger
{
    public static void LogAudit(string userId, string action, string controller)
    {
        Log.ForContext("UserId", userId)
           .ForContext("Action", action)
           .ForContext("Controller", controller)
           .Information("Audit action performed");
    }
}
