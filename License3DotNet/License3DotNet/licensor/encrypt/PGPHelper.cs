﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using System.IO;

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

        private void setHashedSubpackets(PgpSignatureGenerator signatureGenerator) 
        {
            IEnumerator<string> it = (IEnumerator<string>)key.PublicKey.GetUserIds().GetEnumerator();
            while (it.MoveNext())
            {
                PgpSignatureSubpacketGenerator generator = new PgpSignatureSubpacketGenerator();
                generator.SetSignerUserId(false, it.Current);
                signatureGenerator.SetHashedSubpackets(generator.Generate());
            }
        }

        private void init(PgpSignatureGenerator signatureGenerator, PgpPrivateKey pgpPrivKey)
        {
            signatureGenerator.InitSign(PgpSignature.BinaryDocument, pgpPrivKey);
            setHashedSubpackets(signatureGenerator);
        }

        private PgpCompressedDataGenerator compressedDataGenerator() 
        {
            // TODO: not the same in the .net library, search alternative
            // return new PgpCompressedDataGenerator(PgpCompressedData.ZLIB);
        }

        // TODO: Porting of Outputstream
        private void encode(string licensePlain, PgpSignatureGenerator signatureGenerator, MemoryStream outputStream) {
        {
            PgpLiteralDataGenerator literalDataGenerator = new PgpLiteralDataGenerator();
        }

        public void setHashAlgorithm(int hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }

        // Cannot find BCPGOutputstream, searching for alternatives
        public byte[] encodeLicense(string keyPassPhraseString, string licensePlain)
        {
            MemoryStream mem = new MemoryStream();
            StreamWriter writer = new StreamWriter(mem);
            PgpCompressedDataGenerator generator = compressedDataGenerator();
            
        }
    }
}
