using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace License3DotNet.licensor.hardware
{
    class HashCalculator
    {
        private InterfaceSelector selector;
        Encoding utf8 = Encoding.UTF8;

        public HashCalculator(InterfaceSelector selector)
        {
            this.selector = selector;
        }

        private void updateWithNetworkData(MD5Digest md5, List<NetworkInterfaceData> networkInterfaces)
        {
            foreach (NetworkInterfaceData ni in networkInterfaces)
            {
                // could not port 1 to 1, because library is different
                md5.Update(utf8.GetBytes(ni.name)[0]);
                if (ni.hwAddress != null)
                {
                    md5.Update(ni.hwAddress[0]);
                }
            }
        }

        public void updateWithNetworkData(MD5Digest md5)
        {
            List<NetworkInterfaceData> networkInterfaces = NetworkInterfaceData.gatherUsing(selector);
            networkInterfaces.OrderBy(a => a.name);
            updateWithNetworkData(md5, networkInterfaces);
        }

        public void updateWithHostName(MD5Digest md5)
        {
            string hostName = Dns.GetHostName();
            md5.Update(utf8.GetBytes(hostName)[0]);
        }

        public void updateWithArchitecture(MD5Digest md5)
        {
            string architectureString = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            md5.Update(utf8.GetBytes(architectureString)[0]);
        }
    }
}
