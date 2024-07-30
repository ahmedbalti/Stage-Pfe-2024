//using Microsoft.Crm.Sdk.Messages;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Client;
//using Microsoft.Xrm.Sdk.Query;
//using Org.BouncyCastle.Asn1.Ocsp;
//using System;
//using System.Data;
//using System.Net;
//using System.ServiceModel.Description;

//namespace User.Gestion.Service.Services
//{
    
//        public class CrmService
//    {
//        public IOrganizationService _service;
//        public ConnectionState connectionState_;
 
//        public CrmService()
//        {
//            #region Correctif Connexion
//            var _crmUser = "HLICONSULTING\a.balti";
//            var _crmPwd = "Inetum@967#+";
//            var _crmUrl = "https://crm-demo.hlitn.com/DEMOCRMV9/XRMServices/2011/Organization.svc";
 
//            Uri oUri = new Uri(_crmUrl);
//            ClientCredentials clientCredentials = new ClientCredentials();
//            clientCredentials.UserName.UserName = _crmUser;
//            clientCredentials.UserName.Password = _crmPwd;
//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
           
//            OrganizationServiceProxy myOrganizationServiceProxy = new OrganizationServiceProxy(oUri, null, clientCredentials, null);
//            IOrganizationService Instance = myOrganizationServiceProxy;
//            _service = Instance;
//            //connectionState_ = new ConnectionState();
//            /* {
//                 message = "",
//                 service = myOrganizationServiceProxy,
//                 state = true
//             };*/
//            #endregion
//            // Connect();
//        }
 
//        public EntityCollection RetrieveRecords(string entityName, ColumnSet columns)
//        {
//            QueryExpression query = new QueryExpression(entityName);
//            query.ColumnSet = columns;
 
//            return _service.RetrieveMultiple(query);
//        }
 
//    }
//}