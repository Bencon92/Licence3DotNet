using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace License3DotNet.licensor.hardware
{
    class UUIDCalculator
    {
        private HashCalculator calculator;

        public UUIDCalculator(InterfaceSelector selector)
        {
            this.calculator = new HashCalculator(selector);
        }

        public Guid getMachineId(Boolean useNetwork, Boolean useHostName, Boolean useArchitecture)
        {
            MD5Digest md5 = new MD5Digest();
            md5.Reset();
            if (useNetwork)
            {
                calculator.updateWithNetworkData(md5);
            }
            if (useHostName)
            {
                calculator.updateWithHostName(md5);
            }
            if (useArchitecture)
            {
                calculator.updateWithArchitecture(md5);
            }
            byte[] digest = new byte[16];
            md5.DoFinal(digest, 0);
            // Guid class generates different strings than UUID.NameUUIDFromBytes
            return new Guid(digest);
        }

        public string getMachineIdString(Boolean useNetwork, Boolean useHostName, Boolean useArchitecture)
        {
            Guid guid = getMachineId(useNetwork, useHostName, useArchitecture);
            if (guid != null)
            {
                return guid.ToString();
            }
            else
            {
                return null;
            }
        }

        public Boolean assertUUID(Guid uuid, Boolean useNetwork, Boolean useHostName, Boolean useArchitecture)
        {
            Guid machineGuid = getMachineId(useNetwork, useHostName, useArchitecture);
            return machineGuid != null && machineGuid.Equals(uuid);
        }

        public Boolean assertUUID(string uuid, Boolean useNetwork, Boolean useHostName, Boolean useArchitecture)
        {
            try
            {
                return assertUUID(Guid.Parse(uuid), useNetwork, useHostName, useArchitecture);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
