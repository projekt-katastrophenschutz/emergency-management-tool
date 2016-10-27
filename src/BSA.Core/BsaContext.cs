// <copyright file="BsaContext.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core
{
    using System.Net;
    using System.Net.Sockets;
    using BSA.Core.Network;

    public class BsaContext
    {
        private static BsaContext context;
        private ApplicationRole Role;
        private string Url;
        private string UserName;
        
        public BsaContext(string Url, string UserName, ApplicationRole Role)
        {
            this.UserName = UserName;
            this.Url = Url;
            this.Role = Role;
        }

        public static string GetURL()
        {
            if (context == null)
            {
                // ToDo: Make use of parameterless constructor and pass the defaults there
                context = new BsaContext("", "", ApplicationRole.Client);
            }
            return context.Url;
        }

        public static void SetURL(string url)
        {
            if (context == null)
            {
                context = new BsaContext(url, "", ApplicationRole.Client);
            }
            context.Url = url;
        }

        public static string GetUserName()
        {
            if (context == null)
            {
                context = new BsaContext("", "", ApplicationRole.Client);
            }
            return context.UserName;
        }

        public static ApplicationRole GetUserRole()
        {
            if (context == null)
            {
                context = new BsaContext("", "", ApplicationRole.Client);
            }
            return context.Role;
        }
        
        public static void Initialize(string url, string userName, ApplicationRole role)
        {
            if (context == null)
            {
                context = new BsaContext(url, userName, role);
            }
            else
            {
                context.UserName = userName;
                context.Url = url;
                context.Role = role;
            }
        }
    }
}