// <copyright file="CommunicationProxy.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using BSA.Core.Messages;
    using BSA.Core.Models;
    using BSA.Core.Network.Hubs;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Client;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;
    using System.Net;
    using Utils;
    using System.Diagnostics;
    public static class CommunicationProxy
    {
        private static IHubProxy HubProxy;

        // ToDo: Use service interfaces + dependency injection instead of a hard coded implementation
        public static ConnectionJobService jService = ServiceLocator.Current.GetInstance<IJobService>() as ConnectionJobService;
        public static ConnectionForcesService fService = ServiceLocator.Current.GetInstance<IForcesService>() as ConnectionForcesService;

        public static ApplicationRole Role { get; private set; } = ApplicationRole.Server;

        public static HubConnection HubConnection { get; private set; }

        public static bool TrySetRole(ApplicationRole role)
        {
            if (Role == role)
            {
                return true;
            }
            switch (role)
            {
                case ApplicationRole.Client:
                    Role = ApplicationRole.Client;
                    return true;
                case ApplicationRole.Server:
                    // Handle role change in a proper way (e.g. conflict handling -> send a message to current server)
                    Role = ApplicationRole.Server;
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }

        public static async Task SynchronizeAsync()
        {
            var forces = await HubProxy.Invoke<List<Force>>("getForceList");
            var jobs = await HubProxy.Invoke<List<Job>>("getJobsList");

            foreach (var j in jobs)
            {
                jService.SaveJobLocal(j);
            }
            foreach (var f in forces)
            {
                fService.SaveForcesLocal(f);
            }
        }

        public static async Task<bool> ConnectToServer(string serverUrl, uint serverPort)
        {
            try
            {
                HubConnection?.Stop();

                HubConnection = new HubConnection($"http://{serverUrl}:{serverPort}");
                HubProxy = HubConnection.CreateHubProxy("CommunicationHub");

                HubProxy.On<Job>("SendJobToClient", (job) =>
                {
                    jService.SaveJobLocal(job);
                }
                    );

                HubProxy.On<Job>("SendDeleteJobToClient", job => {
                   
                    jService.DeleteJobLocal(job); }
                    );

                HubProxy.On<Force>("SendForceToClient", force => {
                    
                    fService.SaveForcesLocal(force);
                }
               
                    );

                HubProxy.On<Force>("SendDeleteForceToClient", force => { fService.DeleteForcesLocal(force); }
                    );

                HubProxy.On<string, uint>("ChangeServer", async (address, port) =>
                { await ConnectToServer(address, port); }
                    );

                await HubConnection.Start();
                
                HubConnection.Closed += () => handleDisconnect();

            }
            catch (Exception e)
            {
                return false;
            }

       /*     var dialogResult =
                MessageBox.Show(
                    "Die bestehende Datenbank wird gelöscht und synchronisiert, sind Sie damit einverstanden?",
                    "Sicher?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //datenbank löschen
                Synchronize();
            }
            else if (dialogResult == DialogResult.No)
            {
            }*/

            if (HubConnection.State == ConnectionState.Connected)
            {
                TrySetRole(ApplicationRole.Client);
                return true;
            }
            return false;
        }

        private static void handleDisconnect()
        {
            Messenger.Default.Send(new DisconnectedMessage());
        }
            /*      DialogResult r=MessageBox.Show("","Sie sind nicht mehr verbunden, wollen Sie sich neu verbinden?", MessageBoxButtons.YesNo);
                  if (r == DialogResult.Yes)
                  {
                     HubConnection.Start();
                  }
                  else if (r == DialogResult.No)
                  {

                  }*/

        // ToDo: The messenger classes are intendet for inner-application communication to inform the UI about database changes. You can pass multiple arguments instead.
        // ToDo: How do you handle client requests and the corresponding server responses? The UI has to wait until the corresponding entity was created/updated/deleted on the server.

        public static void SendJobChange( Job job)
        {
            if (Role == ApplicationRole.Client)
            {
                HubProxy.Invoke("SendJob", job);
            }
            else
            {
                var jobsHub = GlobalHost.ConnectionManager.GetHubContext<CommunicationHub>();
                jobsHub.Clients.All.SendJobToClient(job);
            }
        }

        public static void SendJobDeleted(Job job)
        {
            if (Role == ApplicationRole.Client)
            {
                HubProxy.Invoke("DeleteJob",job);
            }
            else
            {
                var jobsHub = GlobalHost.ConnectionManager.GetHubContext<CommunicationHub>();
                jobsHub.Clients.All.SendDeleteJobToClient(job);
            }
        }

        public static void SendForceChanged(Force force)
        {
            if (Role == ApplicationRole.Client)
            {
                HubProxy.Invoke("SendForce",force);
            }
            else
            {
                var jobsHub = GlobalHost.ConnectionManager.GetHubContext<CommunicationHub>();
                jobsHub.Clients.All.SendForceToClient(force);
            }
        }

        public static void SendForceDeleted( Force force)
        {
            if (Role == ApplicationRole.Client)
            {
                HubProxy.Invoke("DeleteForce", force);
            }
            else
            {
                var jobsHub = GlobalHost.ConnectionManager.GetHubContext<CommunicationHub>();
                jobsHub.Clients.All.SendDeleteForceToClient(force);
            }
        }

        public static bool PermitServerChange()
        {
            var user = BsaContext.GetUserName();
            return HubProxy.Invoke<bool>("MayGetServer", user).Result;
        }

        public static int AcquireServer(string ipAddress)
        {
           
                // ToDo: Which result do you expect? You need a proper approval/confirmation handling!
                    HubProxy.Invoke<bool>("ShutDownServer", ipAddress, Server.Port);
                       if (UacHelper.IsElevated() == false)
                    {
                        if (UacHelper.RestartWithAdminRights("serverRole") == false)
                        {
                            return 0;
                        }
                        Process.GetCurrentProcess().Kill();
                    }
                    return 1;
                }
        
    }
}