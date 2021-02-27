////=====================================================================================
//// All Rights Reserved , Copyright © MLTechnology 2017-Now
////=====================================================================================
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Security;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace ML.Core.Base.Security
//{
//    public class RSAHelper
//    {
//        private static void CreateKey()
//        {
//            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
//            {
//                string publicKey = rsa.ToXmlString(false); // 公钥
//                string privateKey = rsa.ToXmlString(true); // 私钥               
//            }
//        }

//        //公钥格式的转换
//        public static string RsaPublicKeyToXml(string publicKey)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(publicKey))
//                    return "";
//                if (publicKey.Contains("<RSAKeyValue>"))
//                    return publicKey;
//                RsaKeyParameters publicKeyParam;
//                //尝试进行java格式的密钥读取
//                try
//                {
//                    publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
//                }
//                catch
//                {
//                    publicKeyParam = null;
//                }
//                //非java格式密钥进行pem格式的密钥读取
//                if (publicKeyParam == null)
//                {
//                    try
//                    {
//                        var pemKey = publicKey;
//                        if (!pemKey.Contains("BEGIN RSA PRIVATE KEY"))
//                        {
//                            pemKey = @"-----BEGIN RSA PRIVATE KEY-----
//                           " + publicKey + @"
//                           -----END RSA PRIVATE KEY-----";
//                        }
//                        var array = Encoding.ASCII.GetBytes(pemKey);
//                        var stream = new MemoryStream(array);
//                        var reader = new StreamReader(stream);
//                        var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(reader);
//                        publicKeyParam = (RsaKeyParameters)pemReader.ReadObject();
//                    }
//                    catch
//                    {
//                        publicKeyParam = null;
//                    }
//                }
//                //如果都解析失败，则返回原串
//                if (publicKeyParam == null)
//                    return publicKey;
//                //输出XML格式密钥
//                return string.Format(
//                    "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
//                    Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned())
//                );
//            }
//            catch (Exception)
//            {
//                return "error";
//            }
//        }

//        //私钥格式转换
//        public static string RsaPrivateKeyToXml(string privateKey)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(privateKey))
//                    return "";
//                if (privateKey.Contains("<RSAKeyValue>"))
//                    return privateKey;
//                RsaPrivateCrtKeyParameters privateKeyParam;
//                //尝试进行java格式的密钥读取
//                try
//                {
//                    privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
//                }
//                catch
//                {
//                    privateKeyParam = null;
//                }
//                //非java格式密钥进行pem格式的密钥读取
//                if (privateKeyParam == null)
//                {
//                    try
//                    {
//                        var pemKey = privateKey;
//                        if (!pemKey.Contains("BEGIN RSA PRIVATE KEY"))
//                        {
//                            pemKey = @"-----BEGIN RSA PRIVATE KEY-----
//                           " + privateKey + @"
//                           -----END RSA PRIVATE KEY-----";
//                        }
//                        var array = Encoding.ASCII.GetBytes(pemKey);
//                        var stream = new MemoryStream(array);
//                        var reader = new StreamReader(stream);
//                        var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(reader);
//                        var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
//                        privateKeyParam = (RsaPrivateCrtKeyParameters)keyPair.Private;
//                    }
//                    catch
//                    {
//                        privateKeyParam = null;
//                    }
//                }
//                //如果都解析失败，则返回原串
//                if (privateKeyParam == null)
//                    return privateKey;
//                //输出XML格式密钥
//                return string.Format(
//                    "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
//                    Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
//                    Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned())
//                );
//            }
//            catch (Exception)
//            {
//                //throw PayException.New("RSA私钥密钥格式转换失败");
//                return "RSA私钥密钥格式转换失败";
//            }
//        }


//        /// <summary>
//        /// 公钥加密，私钥解密（包括超长字符串的分段加密解密）
//        /// </summary>
//        /// <param name="encryptedInput"></param>
//        /// <param name="privateKey"></param>
//        /// <returns></returns>
//        public static string RsaEncrypt(string rawInput, string publicKey)
//        {
//            if (string.IsNullOrEmpty(rawInput))
//            {
//                return string.Empty;
//            }

//            if (string.IsNullOrWhiteSpace(publicKey))
//            {
//                throw new ArgumentException("Invalid Public Key");
//            }

//            using (var rsaProvider = new RSACryptoServiceProvider())
//            {
//                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
//                rsaProvider.FromXmlString(publicKey);//载入公钥
//                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
//                var buffer = new byte[bufferSize];
//                using (MemoryStream inputStream = new MemoryStream(inputBytes),
//                     outputStream = new MemoryStream())
//                {
//                    while (true)
//                    { //分段加密
//                        int readSize = inputStream.Read(buffer, 0, bufferSize);
//                        if (readSize <= 0)
//                        {
//                            break;
//                        }

//                        var temp = new byte[readSize];
//                        Array.Copy(buffer, 0, temp, 0, readSize);
//                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
//                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
//                    }
//                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
//                }
//            }
//        }

//        /// <summary>
//        /// 私钥解密
//        /// </summary>
//        /// <param name="encryptedInput"></param>
//        /// <param name="privateKey"></param>
//        /// <returns></returns>
//        public static string RsaDecrypt(string encryptedInput, string privateKey)
//        {
//            if (string.IsNullOrEmpty(encryptedInput))
//            {
//                return string.Empty;
//            }

//            if (string.IsNullOrWhiteSpace(privateKey))
//            {
//                throw new ArgumentException("Invalid Private Key");
//            }

//            using (var rsaProvider = new RSACryptoServiceProvider())
//            {
//                var inputBytes = Convert.FromBase64String(encryptedInput);
//                rsaProvider.FromXmlString(privateKey);
//                int bufferSize = rsaProvider.KeySize / 8;
//                var buffer = new byte[bufferSize];
//                using (MemoryStream inputStream = new MemoryStream(inputBytes),
//                     outputStream = new MemoryStream())
//                {
//                    while (true)
//                    {
//                        int readSize = inputStream.Read(buffer, 0, bufferSize);
//                        if (readSize <= 0)
//                        {
//                            break;
//                        }

//                        var temp = new byte[readSize];
//                        Array.Copy(buffer, 0, temp, 0, readSize);
//                        var rawBytes = rsaProvider.Decrypt(temp, false);
//                        outputStream.Write(rawBytes, 0, rawBytes.Length);
//                    }
//                    return Encoding.UTF8.GetString(outputStream.ToArray());
//                }
//            }
//        }

//        //私钥加密，公钥解密（包括超长字符串的分段加密解密）

//        /// <summary>
//        /// RSA加密 使用私钥加密
//        /// </summary>
//        /// <param name="byteData"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        //private static string RSAEncrypt(string data, string key)
//        //{
//        //    byte[] byteData = Encoding.UTF8.GetBytes(data);
//        //    var privateRsa = GetRsaCryptoFromXml(key);
//        //    //转换密钥  下面的DotNetUtilities来自Org.BouncyCastle.Security
//        //    var keyPair = DotNetUtilities.GetKeyPair(privateRsa);

//        //    var c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");

//        //    c.Init(true, keyPair.Private);//取私钥（true为加密）


//        //    int bufferSize = (privateRsa.KeySize / 8) - 11;//单块最大长度
//        //    var buffer = new byte[bufferSize];
//        //    using (MemoryStream inputStream = new MemoryStream(byteData), outputStream = new MemoryStream())
//        //    {
//        //        while (true)
//        //        { //分段加密
//        //            int readSize = inputStream.Read(buffer, 0, bufferSize);
//        //            if (readSize <= 0)
//        //            {
//        //                break;
//        //            }

//        //            var temp = new byte[readSize];
//        //            Array.Copy(buffer, 0, temp, 0, readSize);
//        //            //var encryptedBytes = rsaProvider.Encrypt(temp, false);
//        //            var encryptedBytes = c.DoFinal(temp);
//        //            outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
//        //        }
//        //        return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
//        //    }

//        //}

//        /// <summary>
//        /// RSA解密 使用公钥解密
//        /// </summary>
//        /// <param name="byteData"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        //private static string RSADecrypt(string data, string key)
//        //{
//        //    byte[] byteData = Convert.FromBase64String(data);
//        //    var privateRsa = GetRsaCryptoFromXml(key);
//        //    //转换密钥  
//        //    var keyPair = DotNetUtilities.GetRsaPublicKey(privateRsa);

//        //    var c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");

//        //    c.Init(false, keyPair);//取公钥（false为解密）

//        //    using (MemoryStream inputStream = new MemoryStream(byteData), outputStream = new MemoryStream())
//        //    {
//        //        int restLength = byteData.Length;
//        //        while (restLength > 0)
//        //        {
//        //            int readLength = restLength < 128 ? restLength : 128;
//        //            restLength = restLength - readLength;
//        //            byte[] readBytes = new byte[readLength];
//        //            inputStream.Read(readBytes, 0, readLength);
//        //            byte[] append = c.DoFinal(readBytes);
//        //            outputStream.Write(append, 0, append.Length);
//        //        }
//        //        //注意，这里不一定就是用utf8的编码方式,这个主要看加密的时候用的什么编码方式
//        //        return Encoding.UTF8.GetString(outputStream.ToArray());
//        //    }

//        //}
//    }
//}
