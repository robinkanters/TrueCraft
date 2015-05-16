using System;
using System.Linq;
using TrueCraft.Core.Windows;
using TrueCraft.API;
using TrueCraft.API.Networking;

namespace TrueCraft.Commands
{
    public class OpCommand : Command
    {
        public override string Name
        {
            get { return "op"; }
        }

        public override string Description
        {
            get { return "Give the specified player operator privileges."; }
        }

        public override string[] Aliases
        {
            get { return new string[]{}; }
        }

        public override void Handle(IRemoteClient client, string alias, string[] arguments)
        {
            if (arguments.Length < 1)
            {
                Help(client, alias, arguments);
                return;
            }

            var username = arguments[0].Trim();
            if (username.Contains(' ') || username.Contains('-') || username.Contains('|') || username.Contains(','))
            {
                client.SendMessage(string.Format("Username '{0}' is invalid", username));
                return;
            }

            if (client.Server.PlayerIsOp(username))
            {
                client.SendMessage(string.Format("Player '{0}' already has Op privileges!", username));
                return;
            }

            OpPlayer(username, client);

            client.SendMessage(string.Format("Player '{0}' now has Op privileges!", username));
        }

        private static void OpPlayer(string receivingPlayer, IRemoteClient client)
        {
            client.Server.AccessConfiguration.Oplist.Add(receivingPlayer);
        }

        protected static IRemoteClient GetPlayerByName(IRemoteClient client, string username)
        {
            var receivingPlayer =
                client.Server.Clients.FirstOrDefault(
                    c => String.Equals(c.Username, username, StringComparison.CurrentCultureIgnoreCase));
            return receivingPlayer;
        }

        public override void Help(IRemoteClient client, string alias, string[] arguments)
        {
            client.SendMessage("Correct usage is /" + alias + " <User> <Item ID> [Amount]");
        }
    }
}
