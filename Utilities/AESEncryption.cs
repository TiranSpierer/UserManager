/*
    UserManager/Utilities/AESEncryption.cs
    Created by Tiran Spierer
    Created at 05/01/2023
    Class propose:  Provides methods to encrypt and decrypt strings using the AES (Advanced Encryption Standard) algorithm. 
                    The Encrypt method takes a plaintext string as input and returns a ciphertext string that consists of three parts: 
                        - the base64-encoded versions of the key
                        - initialization vector (IV)
                        - ciphertext
                    separated by '|' characters.

                    The Decrypt method takes a ciphertext string as input and returns the original plaintext string.

                    The EncryptStringToBytes and DecryptStringFromBytes methods are private helper methods that do the actual 
                    encryption and decryption using the .NET Framework's Aes class. They take byte arrays representing the key 
                    and IV as inputs, as well as the plaintext or ciphertext to be encrypted or decrypted. They return the 
                    encrypted or decrypted data as a byte array.
*/

using System;
using System.IO;
using System.Security.Cryptography;

namespace Utilities;

public static class AesEncryption
{

#region Public Properties

    public static string Encrypt(string stringToEncrypt)
    {
        using var aes           = Aes.Create();
        var       encrypted = EncryptStringToBytes(stringToEncrypt, aes.Key, aes.IV);

        var keyString       = Convert.ToBase64String(aes.Key);
        var ivString        = Convert.ToBase64String(aes.IV);
        var encryptedString = Convert.ToBase64String(encrypted);
        var combinedString  = keyString + '|' + ivString + '|' + encryptedString;

        return combinedString;
    }

    public static string Decrypt(string cipherText)
    {
        var parts     = cipherText.Split('|');
        var key       = Convert.FromBase64String(parts[0]);
        var iv        = Convert.FromBase64String(parts[1]);
        var encrypted = Convert.FromBase64String(parts[2]);

        return DecryptStringFromBytes(encrypted, key, iv);
    }


    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private static byte[] EncryptStringToBytes(string stringToEncrypt, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (stringToEncrypt == null || stringToEncrypt.Length <= 0)
            throw new ArgumentNullException(nameof(stringToEncrypt));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));
        if (iv == null || iv.Length <= 0)
            throw new ArgumentNullException(nameof(iv));
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV  = iv;

        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using MemoryStream msEncrypt = new();
        using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(stringToEncrypt);
        }
        encrypted = msEncrypt.ToArray();

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));
        if (iv == null || iv.Length <= 0)
            throw new ArgumentNullException(nameof(iv));

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV  = iv;

        // Create a decryptor to perform the stream transform.
        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using MemoryStream msDecrypt = new(cipherText);
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);

        // Read the decrypted bytes from the decrypting stream
        // and place them in a string.
        plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }


#endregion
}