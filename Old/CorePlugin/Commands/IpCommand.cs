using System.Net;
using RebusCore.Commands;

namespace CorePlugin.Commands
{
    public class IpCommand : ICommand
    {
        public bool Execute(string command)
        {
            if (command.Contains("/ip"))
            {
                Result = "ADDRESSES: \r\n";
                
                foreach (IPAddress ipAddress in Dns.GetHostAddresses("localhost"))
                {
                    Result += ipAddress.ToString() + "\r\n";
                }
                
                return true;
            }


            return false;
        }

        public string Result { get; set; }
    }
}