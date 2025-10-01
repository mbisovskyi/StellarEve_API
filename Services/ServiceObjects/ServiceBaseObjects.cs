using System.Diagnostics;

namespace StellarEve_API.Services.ServiceBaseObjects
{
    public class ServiceBaseResponse 
    {
        public bool Success { get; set; } = true;
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
    }
}
