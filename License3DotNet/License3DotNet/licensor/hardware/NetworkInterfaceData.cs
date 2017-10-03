using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;

/**
 * A data class holding the network interface data.
 *
 */
namespace License3DotNet.licensor.hardware
{
    class NetworkInterfaceData
    {
        public String name;
        public byte[] hwAddress;

        private NetworkInterfaceData(NetworkInterface networkInterface)
        {
            name = networkInterface.Name;
            try
            {
                hwAddress = networkInterface.GetPhysicalAddress().GetAddressBytes();
            } catch(SocketException e) {
                throw new SystemException(e.ToString(), e);
            }
        }

        public static List<NetworkInterfaceData> gatherUsing(InterfaceSelector selector)
        {
            // TODO: port to C#
            //return Collections.list(NetworkInterface.getNetworkInterfaces()).stream()
            //.filter(selector::usable)
            //.map(NetworkInterfaceData::new).collect(Collectors.toList());
            return null;
        }
    }
}
