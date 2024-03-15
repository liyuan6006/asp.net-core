namespace WebApplication1Test.Extensions
{
    /// <summary>Commonly used environment names.</summary>
    public static class EnvironmentName
    {
        public static readonly string Local = "local";
        public static readonly string Dev = "dev";
        public static readonly string QA = "qa";
        public static readonly string Prod = "prod";
    }
}

namespace WebApplication1Test.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Checks if the current hosting environment name is "local".
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is "Development", otherwise false.</returns>
        public static bool IsLocalEnv(this Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException("hostingEnvironment");
            return hostingEnvironment.IsEnvironment(EnvironmentName.Local);
        }

        /// <summary>
        /// Checks if the current hosting environment name is "dev".
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is "Development", otherwise false.</returns>
        public static bool IsDevEnv(this Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException("hostingEnvironment");
            return hostingEnvironment.IsEnvironment(EnvironmentName.Dev);
        }

        /// <summary>
        /// Checks if the current hosting environment name is "qa".
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is "Staging", otherwise false.</returns>
        public static bool IsQAEnv(this Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException("hostingEnvironment");
            return hostingEnvironment.IsEnvironment(EnvironmentName.QA);
        }

        /// <summary>
        /// Checks if the current hosting environment name is "prod".
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if the environment name is "Production", otherwise false.</returns>
        public static bool IsProdEnv(this Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException("hostingEnvironment");
            return hostingEnvironment.IsEnvironment(EnvironmentName.Prod);
        }

        /// <summary>
        /// Compares the current hosting environment name against the specified value.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <param name="environmentName">Environment name to validate against.</param>
        /// <returns>True if the specified name is the same as the current environment, otherwise false.</returns>
        public static bool IsEnvironment(this Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, string environmentName)
        {
            if (hostingEnvironment == null)
                throw new ArgumentNullException("hostingEnvironment");
            return string.Equals(hostingEnvironment.EnvironmentName, environmentName, StringComparison.OrdinalIgnoreCase);
        }
    }
}