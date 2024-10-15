using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InstituteManagement
{
    public static class CCryptography
    {
        /// <summary>
        ///    Decrypts  a particular string with a specific Key
        /// </summary>
        public static string Decrypt(string a_sStringToDecrypt, string a_sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;

            if (a_sStringToDecrypt == null)
            {
                return (string.Empty);
            }
            inputByteArray = new byte[a_sStringToDecrypt.Length];

            try
            {
                key = Encoding.UTF8.GetBytes(a_sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(a_sStringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception)
            {

                return (string.Empty);
            }
        }

        /// <summary>
        ///   Encrypts  a particular string with a specific Key
        /// </summary>
        /// <param name="stringToEncrypt"></param>
        /// <param name="sEncryptionKey"></param>
        /// <returns></returns>
        public static string Encrypt(string a_sStringToEncrypt, string a_sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length) 

            try
            {
                key = Encoding.UTF8.GetBytes(a_sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(a_sStringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (System.Exception)
            {
                return (string.Empty);
            }
        }

        public static string encryptByMod31(string a_sStringToEncrypt)
        {
            char[] aPasswordChar = a_sStringToEncrypt.ToCharArray();
            int iLength = aPasswordChar.Length;
            char[] aNewPass = new char[iLength];
            StringBuilder sbEncrypted = new StringBuilder();
            char cTemp;
            byte byChar;
            byte byModOf = 31;

            for (int iCtr = 0; iCtr < iLength; iCtr++)
            {
                byChar = (byte)(aPasswordChar[iCtr]);
                cTemp = (char)(byChar % byModOf);
                aNewPass[iCtr] = cTemp;
                sbEncrypted.Append(cTemp);
            }

            return (getASCIIValues(sbEncrypted.ToString()));
        }

        private static string getASCIIValues(string a_sInputString)
        {

            char[] aChar = a_sInputString.ToCharArray();
            int iValue;
            StringBuilder sbReturn = new StringBuilder();
            int iLength = aChar.Length;

            for (int iCtr = 0; iCtr < iLength; iCtr++)
            {
                iValue = (int)aChar[iCtr];
                sbReturn.Append(iValue.ToString("00"));
            }

            return (sbReturn.ToString());
        }

    }

}