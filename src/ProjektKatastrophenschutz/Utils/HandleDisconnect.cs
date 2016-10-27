// <copyright file="HandleDisconnect.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Utils
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using BSA.Core;
    using BSA.Core.Messages;
    using BSA.Core.Network;
    using BSA.Core.Utils;
    using GalaSoft.MvvmLight.Messaging;

    public class HandleDisconnect
    {
        public HandleDisconnect()
        {
            Messenger.Default.Register<DisconnectedMessage>(this,
                s => { var unused = new DisconnectMessageBox(); });
        }
        
        private class DisconnectMessageBox : Form
        {
            readonly Button reconnectButton = new Button();
            readonly Button getServerButton = new Button();
            readonly Button connectOtherButton = new Button();

            public DisconnectMessageBox()
            {
                this.reconnectButton.Text = @"Erneut verbinden";
                this.getServerButton.Text = @"Server werden";
                this.connectOtherButton.Text = @"Mit anderem Server verbinden";

                this.reconnectButton.Click += async (a, b) =>
                    {
                        this.ChangeButtonIsEnabled(false);
                        var success = await CommunicationProxy.ConnectToServer(BsaContext.GetURL(), 8081);
                        if (success)
                        {
                            this.Close();
                        }

                        this.ChangeButtonIsEnabled(true);
                    };

                this.getServerButton.Click += (a, b) =>
                {
                    this.ChangeButtonIsEnabled(false);
                    if (UacHelper.RestartWithAdminRights("") == false)
                    {
                        this.ChangeButtonIsEnabled(true);
                        return;
                    }

                    Process.GetCurrentProcess().Kill();
                };

                this.connectOtherButton.Click += async (a, b) =>
                {
                    this.ChangeButtonIsEnabled(false);
                    var success = await this.ConnectToServerAsync();
                    if (success == false)
                    {
                        this.ChangeButtonIsEnabled(true);
                    }
                    else
                    {
                        this.Close();
                    }
                };

                var l = new Label {Text = @"Die Verbindung zum Server ist getrennt. Was möchten Sie jetzt tun?"};
                l.SetBounds(100, 30, 450, 50);
                this.reconnectButton.SetBounds(50, 100, 150, 50);
                this.getServerButton.SetBounds(250, 100, 150, 50);
                this.connectOtherButton.SetBounds(450, 100, 150, 50);

                this.connectOtherButton.Anchor = this.connectOtherButton.Anchor | AnchorStyles.Right;
                this.reconnectButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                this.getServerButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                this.Text = @"Fehler";
                this.ClientSize = new Size(650, 200);
                this.Controls.AddRange(new Control[] {l, this.reconnectButton, this.getServerButton, this.connectOtherButton});
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.MinimizeBox = false;
                this.MaximizeBox = false;

                this.ShowDialog();
            }

            private void ChangeButtonIsEnabled(bool isEnabled)
            {
                this.reconnectButton.Enabled = isEnabled;
                this.getServerButton.Enabled = isEnabled;
                this.connectOtherButton.Enabled = isEnabled;
            }

            private async Task<bool> ConnectToServerAsync()
            {
                string ipAddress = string.Empty;
                var result = this.InputBox("IP angeben", "Bitte geben Sie die neue IP Adresse an", ref ipAddress);
                if (result == DialogResult.OK)
                {
                    if (await CommunicationProxy.ConnectToServer(ipAddress, 8081) == false)
                    {
                        MessageBox.Show(
                            @"Die Anwendung konnte nicht mit dem angegebenen Server verbunden werden! Bitte überprüfen Sie ihre Angaben!");
                    }
                    else
                    {
                        return true;
                    }
                }

                return false;
            }

            private DialogResult InputBox(string title, string promptText, ref string value)
            {
                var form = new Form();
                var label = new Label();
                var textBox = new TextBox();
                var buttonOk = new Button();
                var buttonCancel = new Button();

                form.Text = title;
                label.Text = promptText;
                textBox.Text = value;

                buttonOk.Text = @"OK";
                buttonCancel.Text = @"Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(100, 20, 200, 20);
                textBox.SetBounds(100, 50, 300, 60);
                buttonOk.SetBounds(100, 100, 100, 40);
                buttonCancel.SetBounds(300, 100, 100, 40);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(500, 170);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                //form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                var dialogResult = form.ShowDialog();
                value = textBox.Text;
                return dialogResult;
            }
        }
    }
}