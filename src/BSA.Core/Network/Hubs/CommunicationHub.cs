// <copyright file="CommunicationHub.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Network.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using BSA.Core.Models;
    using BSA.Core.Services;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Practices.ServiceLocation;
    using System.Threading;
    using System.ComponentModel;
    public class CommunicationHub : Hub
    {
        public void SendJob(Job job)
        {
            var jobService = ServiceLocator.Current.GetInstance<IJobService>() as ConnectionJobService;
            this.Clients.All.SendJobToClient(job);
            jobService?.SaveJobLocal(job);
        }

        public void DeleteJob(Job job)
        {
            var jobService = ServiceLocator.Current.GetInstance<IJobService>() as ConnectionJobService;
            this.Clients.All.SendDeleteJobToClient(job);
            jobService?.DeleteJob(job);
        }

        public void SendForce(Force force)
        {
            var forceService = ServiceLocator.Current.GetInstance<IForcesService>() as ConnectionForcesService;
            this.Clients.All.SendForceToClient(force);
            forceService?.SaveForcesLocal(force);
        }

        public void DeleteForce(Force force)
        {
            var forceService = ServiceLocator.Current.GetInstance<IForcesService>() as ConnectionForcesService;
            this.Clients.All.SendDeleteForceToClient(force);
            forceService?.DeleteForcesLocal(force);
        }

        public bool MayGetServer(string user)
        {
            var dialogResult = MessageBox.Show( $"Der Nutzer {user} möchte Server werden, erlauben Sie das?", "Sicher?",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            if (dialogResult == DialogResult.No)
            {
                return false;
            }
            return false;
        }

        public List<Force> getForceList()
        {
            var forcesService = ServiceLocator.Current.GetInstance<IForcesService>();
            return forcesService.GetAllForces();
        }

        public List<Job> getJobsList()
        {
            var jobService = ServiceLocator.Current.GetInstance<IJobService>();
            return jobService.GetAllJobs();
        }


        public async Task<bool> ShutDownServer(string ip, uint port)
        {
            try
            {
                
                Thread.Sleep(30000);
                this.Clients.All.ChangeServer(ip, port);
                Server.ShutDown();
                if (await CommunicationProxy.ConnectToServer(ip, port)){
                BsaContext.Initialize(ip, BsaContext.GetUserName(), ApplicationRole.Client);
                MessageBox.Show("Sie sind jetz nicht mehr Server, sondern nur noch Client!");
                return true;
                }
                else
                {
                    MessageBox.Show("Der Wechsel hat nicht funktioniert! Bitte starten Sie neu und verbinden sich manuell!");
                    Cursor.Current = Cursors.Default;
                    return false;
                }
            }
            catch (SystemException e)
            {
                return false;
            }
        }
    }
}