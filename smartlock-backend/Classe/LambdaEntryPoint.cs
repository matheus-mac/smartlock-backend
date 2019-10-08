using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartlock_backend.LambdaEntryPoint
{
    using Amazon.Lambda.AspNetCoreServer;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;

    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init (IWebHostBuilder builder)
        {
            builder.
                UseContentRoot(Directory.GetCurrentDirectory()).
                UseStartup<StartupBase>().
                UseLambdaServer();    
        }
    }
}