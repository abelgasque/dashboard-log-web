using Microsoft.SqlServer.Server;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace App.Util
{
    public interface ICryptoHelper
    {
        string InitVector { get; set; }
        string PassPhrase { get; set; }

        ICryptoHelper Init(string pPassPhrase, string pInitVector);

        System.Data.SqlTypes.SqlString Encrypt(System.Data.SqlTypes.SqlString pTextToEncrypt);

        System.Data.SqlTypes.SqlString Decrypt(System.Data.SqlTypes.SqlString pTextToDecrypt);
    }

    internal class CryptoHelper : ICryptoHelper
    {
        private const string SALT_VALUE = "51sdfs45j3";
        private const int PASSWORD_ITERATIONS = 2;
        private const int KEY_SIZE = 256;
        private const string HASH_ALGORITHM = "SHA1";

        private string _PassPhrase = "dashboardalogwebapp";
        private string _InitVector = "@1B2c3D4e5F6g7H8";

        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;
        private ICryptoHelper appCrypto = null;

        public string PassPhrase
        {
            get { return _PassPhrase; }
            set { _PassPhrase = value; }
        }

        public string InitVector
        {
            get { return _InitVector; }
            set { _InitVector = value; }
        }

        public CryptoHelper()
        {

        }

        public ICryptoHelper Init(string pPassPhrase, string pInitVector)
        {
            if (appCrypto == null)
            {
                appCrypto = new CryptoHelper();
            }

            if (pPassPhrase != null) PassPhrase = pPassPhrase;
            if (pInitVector != null) InitVector = pInitVector;

            SymmetricAlgorithm symmetricKey;
            symmetricKey = Aes.Create();

            symmetricKey.Mode = CipherMode.CBC;

            byte[] passwordBytes = GeneratePassword();
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            
            encryptor = symmetricKey.CreateEncryptor(passwordBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(passwordBytes, initVectorBytes);

            return appCrypto;
        }


        private byte[] GeneratePassword()
        {
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(SALT_VALUE);
            PasswordDeriveBytes password = new PasswordDeriveBytes(_PassPhrase,
                                                            saltValueBytes,
                                                            HASH_ALGORITHM,
                                                            PASSWORD_ITERATIONS);
            return password.GetBytes(KEY_SIZE / 8);
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, SystemDataAccess = SystemDataAccessKind.Read)]
        public System.Data.SqlTypes.SqlString Encrypt(System.Data.SqlTypes.SqlString pTextToEncrypt)
        {
            if (pTextToEncrypt.IsNull)
            {
                return null;
            }

            try
            {
                return new System.Data.SqlTypes.SqlString(Encrypt(pTextToEncrypt.ToString()));
            }
            catch (CryptographicException CEx)
            {
                throw new Exception("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception("ERROR: \n" + Ex.Message);
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, SystemDataAccess = SystemDataAccessKind.Read)]
        public System.Data.SqlTypes.SqlString Decrypt(System.Data.SqlTypes.SqlString pTextToDecrypt)
        {
            if (pTextToDecrypt.IsNull)
            {
                return null;
            }

            try
            {

                return new System.Data.SqlTypes.SqlString(Decrypt(pTextToDecrypt.ToString()));
            }
            catch (CryptographicException CEx)
            {
                throw new Exception("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception("ERROR: \n" + Ex.Message);
            }
        }

        public static string Encrypt(string pTextToEncrypt)
        {
            CryptoHelper objCrypto = new CryptoHelper();

            objCrypto.Init("aytyaytypasswordpassword", "@1B2c3D4e5F6g7H8");

            string plainText = pTextToEncrypt;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         objCrypto.encryptor,
                                                         CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        public static string Decrypt(string pTextToDecrypt)
        {
            CryptoHelper objCrypto = new CryptoHelper();

            objCrypto.Init("aytyaytypasswordpassword", "@1B2c3D4e5F6g7H8");

            string cipherText = pTextToDecrypt;

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          objCrypto.decryptor,
                                                          CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }
    }
}
