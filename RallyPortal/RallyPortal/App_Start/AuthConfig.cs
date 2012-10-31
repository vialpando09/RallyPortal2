using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using RallyPortal.Models;

namespace RallyPortal
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "aCXD2otYtB3D8p73YfwkPw",
                consumerSecret: "SBESd1vXj3Og5J3v1HVJW564OUTZ2ClJn2wy9CCDYyo");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "379528372129877",
                appSecret: "0acf2b3aaa861030f91e1f640f50b1fe");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
