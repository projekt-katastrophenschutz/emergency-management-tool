// <copyright file="NetworkClient.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Network
{
    using System;
    using Microsoft.Owin.Hosting;

    public static class Server
    {
        public static string Address { get; private set; } // = "localhost";//"192.168.2.122";

        public static uint Port { get; private set; } = 8081;

        public static bool Running { get; private set; }

        public static IDisposable server;

        // Start server role
        public static String Start(string address)
        {
            String url = $"http://{address}:{Port}";
           server= WebApp.Start(url);

            Address = address;
            Running = true;
            return url;
        }

        public static void ShutDown()
        {
            server.Dispose();
        }
    }
}