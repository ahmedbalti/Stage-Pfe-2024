//using Microsoft.Extensions.Configuration;
//using Microsoft.Rest;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.PowerPlatform.Dataverse.Client;
//using Microsoft.Extensions.Configuration;

//namespace User.Gestion.Service.Services
//{
//    public class Dynamics365Service
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ServiceClient _serviceClient;

//        public Dynamics365Service(IConfiguration configuration)
//        {
//            _configuration = configuration;

//            var connectionString = $"AuthType=OAuth;Url={_configuration["Dynamics365:Url"]};" +
//                                   $"Username={_configuration["Dynamics365:Username"]};" +
//                                   $"Password={_configuration["Dynamics365:Password"]};" +
//                                   "AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=http://localhost;LoginPrompt=Auto";

//            _serviceClient = new ServiceClient(connectionString);
//        }

//        public ServiceClient GetServiceClient() => _serviceClient;
//    }
//}
