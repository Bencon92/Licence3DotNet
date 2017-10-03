using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace License3DotNet.licensor.hardware
{
    class NetworkInterfaceData
    {
        public String name;
        byte[] hwAddress;

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
    }
}
