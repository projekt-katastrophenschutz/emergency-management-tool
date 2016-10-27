// <copyright file="AddressResolving.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Network
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    public static class AddressResolving
    {
        public static IEnumerable<IPAddress> GetIp4Addresses()
        {
            var host = Dns.GetHostName();
            var hostAddresses = Dns.GetHostAddresses(host);
            return
                hostAddresses
                .Where(hostAddress => hostAddress.AddressFamily == AddressFamily.InterNetwork)
                .Where(hostAddress => IPAddress.IsLoopback(hostAddress) == false)
                .OrderBy(x => x.ToString());
        }

        public static string GetIp4AddressesString()
            => GetIp4Addresses().Aggregate(string.Empty, (current, address)
                => current + (current == string.Empty ? $"{address}" : $" {address}"));
    }
}