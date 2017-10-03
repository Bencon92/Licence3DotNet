using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Bcpg.Attr;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;

namespace License3DotNet.licensor.encrypt
{
    class PGPHelper
    {
        private int hashAlgorithm = (int)Org.BouncyCastle.Bcpg.HashAlgorithmTag.Sha512;
        private PgpSecretKey key = null;

        public static Boolean keyIsAppropriate(PgpSecretKey key, string userId, string keyUserId) 
        {
            return key.IsSigningKey && (userId == null || userId.Equals(keyUserId));
        }

        public void setKey(PgpSecretKey key)
        {
            this.key = key;
        }

        private PgpSignatureGenerator signatureGenerator(string keyPassPhrase)
        {
            PgpSignatureGenerator generator = new PgpSignatureGenerator(key.PublicKey.Algorithm, Org.BouncyCastle.Bcpg.HashAlgorithmTag.Sha512);
            PgpPrivateKey privateKey = extractPGPPrivateKey(keyPassPhrase.ToCharArray());

            init(generator, PgpPrivateKey);
            return generator;
        }

        private PgpPrivateKey extractPGPPrivateKey(char[] keyPassPhrase)
        {
            
        }
    }
}
