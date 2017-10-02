using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace License3DotNet.licensor.hardware
{
    class InterfaceSelector
    {
        private HashSet<string> allowedInterfaceNames = new HashSet<string>();
        private HashSet<string> deniedInterfaceNames = new HashSet<string>();

        /**
         * @param string   to match
         * @param regexSet regular expressions provided as set of strings
         * @return true if the {@code string} matches any of the regular expressions
         */
        private static Boolean matchesAny(string chars, HashSet<string> regexSet)
        {
            return regexSet.Any<string>(c => c.Equals(chars));
        }

        /**
         * Checks the sets of regular expressions against the display name of the
         * network interface. If there is a set of denied names then if any of the
         * regular expressions matches the name of the interface then the interface
         * is denied. If there is no denied set then the processing is not affected
         * by the non existence. In other word not specifying any denied interface
         * name means that no interface is denied explicitly.
         * <p>
         * If there is a set of permitted names then if any of the regular
         * expressions matches the name of the interface then the interface is
         * permitted. If there is no set then the interface is permitted. In other
         * words it is not possible to deny all interfaces specifying an empty set.
         * Although this would mathematically logical, but there is no valuable use
         * case that would require this feature.
         * <p>
         * Note that the name, which is checked is not the basic name (e.g.
         * <tt>eth0</tt>) but the display name, which is more human readable.
         *
         * @param netIf the netrowk interface
         * @return {@code true} if the interface has to be taken into the
         * calculation of the license and {@code false} (ignore the
         * interface) otherwise.
         */
        private Boolean matchesRegexLists(NetworkInterface netIf)
        {
            string name = netIf.Name;

            return !matchesAny(name, deniedInterfaceNames) 
                && 
                (!(allowedInterfaceNames.Count() < 1) || 
                        matchesAny(name, allowedInterfaceNames));
        }

        public void interfaceAllowed(string regex)
        {
            allowedInterfaceNames.Add(regex);
        }

        public void interfaceDenied(string regex)
        {
            deniedInterfaceNames.Add(regex);
        }

        /**
         * @param netIf the network interface
         * @return {@code true} if the actual network interface has to be used for
         * the calculation of the hardware identification id.
         */
        Boolean usable(NetworkInterface netIf)
        {
            try
            {
                return !(netIf.NetworkInterfaceType == NetworkInterfaceType.Loopback) 
                    && (netIf.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) <= 0 || netIf.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) <= 0)
                    && !(netIf.NetworkInterfaceType == NetworkInterfaceType.Ppp)
                    && matchesRegexLists(netIf);
            } catch (SocketException e) {
                throw new SystemException(e.ToString(), e);
            }
        }
    }
}
