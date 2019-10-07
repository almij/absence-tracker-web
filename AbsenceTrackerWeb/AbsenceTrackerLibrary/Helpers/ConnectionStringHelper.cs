using System;

namespace AbsenceTrackerLibrary.Helpers
{
    public static class ConnectionStringsHelper
    {
        public static string GetEnvironmentConnectionString()
        {
            var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            bool isUrl = Uri.TryCreate(dbUrl, UriKind.Absolute, out Uri url);
            if (!isUrl)
            {
                throw new UriFormatException("DATABASE_URL environment variable content isn't a valid Url");
            }
            return $"host={url.Host};" +
                $"username={url.UserInfo.Split(':')[0]};" +
                $"password={url.UserInfo.Split(':')[1]};" +
                $"database={url.LocalPath.Substring(1)};" +
                $"pooling=true;" +
                $"SSL Mode=Require;" +
                $"TrustServerCertificate=True;";
        }
    }
}
