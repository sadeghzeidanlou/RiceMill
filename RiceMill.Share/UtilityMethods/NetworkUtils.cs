using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Shared.UtilityMethods
{
    public sealed class NetworkUtils
    {
        public static byte[] GetRealLocalIpAddress()
        {
            UnicastIPAddressInformation? mostSuitableIp = null;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;

                var properties = network.GetIPProperties();
                if (properties.GatewayAddresses.Count == 0)
                    continue;

                foreach (UnicastIPAddressInformation address in properties.UnicastAddresses.OfType<UnicastIPAddressInformation>())
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork || IPAddress.IsLoopback(address.Address))
                        continue;

                    var dnsProperties = network.GetIPProperties().DnsSuffix;
                    if (!string.IsNullOrEmpty(dnsProperties))
                    {
                        mostSuitableIp = address;
                        break;
                    }
                    mostSuitableIp ??= address;
                }
            }
            return mostSuitableIp?.Address.GetAddressBytes() ?? new byte[] { 127, 0, 0, 1 };
        }

        public static void EnableAdapter(string interfaceName)
        {
            var processStartInfo = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            var process = new Process { StartInfo = processStartInfo };
            process.Start();
        }

        public static void DisableAdapter(string interfaceName)
        {
            var processStartInfo = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            var process = new Process { StartInfo = processStartInfo };
            process.Start();
        }

        public static bool PingHost(string nameOrAddress, int timeOut = 1000)
        {
            var ping = new Ping();
            try
            {
                PingReply reply = ping.Send(string.Join(".", nameOrAddress.Split('.').Select(int.Parse).ToList()), timeOut);
                if (reply != null)
                    return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            { }
            finally
            {
                ping?.Dispose();
            }
            return false;
        }
    }
}