using System;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;

namespace User.Gestion.Data.Models
{
    public class CRMConnection  // Removed static keyword
    {
        private string _protocol = "https"; // Made instance variable
        private string ServerUrl = "org4c363b6f.crm4.dynamics.com"; // Made instance variable
        private string _crmUser = "admin@CRM409538.onmicrosoft.com"; // Made instance variable
        private string _crmPwd = "07pWDU5btC;^(7+i"; // Made instance variable

        public CrmServiceClient GetOrgServiceProxy()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var URI = _protocol + "://" + ServerUrl;

            var connectionString = @"AuthType=OAuth;Url=" + URI + ";Username=" + _crmUser + ";Password=" + _crmPwd +
                                   ";AppId=11ab3492-b83c-406c-8dbc-ed9f422fb748" + // Your Application ID
                                   ";LoginPrompt=Auto;RedirectUri=https://login.microsoftonline.com/common/oauth2/nativeclient";

            var serviceProxy = new CrmServiceClient(connectionString);
            return serviceProxy;
        }
    }
}
