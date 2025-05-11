namespace Common.src.Helpers
{
    public static class EnvironmentUtils
    {
        public static bool IsRunningInDocker()
        {
            try
            {
                return File.Exists("/.dockerenv") ||
                       File.ReadAllText("/proc/1/cgroup").Contains("docker");
            }
            catch
            {
                return false;
            }
        }

        public static string GetRabbitMqHost()
        {
            try
            {
                var hostname = File.Exists("/.dockerenv") ||
                       File.ReadAllText("/proc/1/cgroup").Contains("docker") ||
                       Environment.GetEnvironmentVariable("RABBITMQ_HOST") is null ? "localhost" 
                       : Environment.GetEnvironmentVariable("RABBITMQ_HOST");

                return "";
            }
            catch
            {
                return "localhost";
            }
        }
    }

}
