using System.Text;
using DZen.Security.Cryptography;
using Secp256k1Net;

namespace ETHVanitySharp.Utilities;

public static class Utilities
{
    private static Dictionary<char, int> hexCharToNumeric = new()
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3},
        {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7},
        {'8', 8}, {'9', 9}, {'a', 10}, {'b', 11},
        {'c', 12}, {'d', 13}, {'e', 14}, {'f', 15}
    };
   public static int[] HexStringToNumericArray(string hexString)
    {
        hexString = hexString.ToLower();

 

        var numericArray = new int[hexString.Length];
        for (var i = 0; i < hexString.Length; i++)
        {
            if (hexCharToNumeric.TryGetValue(hexString[i], out var numericValue))
            {
                numericArray[i] = numericValue;
            }
            else
            {
                throw new ArgumentException("Invalid hex character: " + hexString[i]);
            }
        }

        return numericArray;
    }
    public static int CharToNumeric(char hexChar)
    {
        const string hexDigits = "0123456789abcdef";

        var index = hexDigits.IndexOf(hexChar);

        if (index != -1)
        {
            return index;
        }

        throw new ArgumentException("Invalid hexadecimal character: " + hexChar);
    }

    public static byte[] GetRandomBytes(int length)
    {
        var result = new byte[length];
        Random.Shared.NextBytes(result);
        return result;
    }


   public static string PrivateToAddress(string privateKey, byte[] candidatePrivateKeyBytes ,ref Secp256k1 secp256k1)
    {
        using var sha3 = SHA3.Create();

        Span<byte> publicKeyBytes = stackalloc byte[Secp256k1.PUBKEY_LENGTH];
        secp256k1.PublicKeyCreate(publicKeyBytes, candidatePrivateKeyBytes);

        var serializedUncompressedPublicKey = new byte[Secp256k1.SERIALIZED_UNCOMPRESSED_PUBKEY_LENGTH];
        secp256k1.PublicKeySerialize(serializedUncompressedPublicKey, publicKeyBytes, Flags.SECP256K1_EC_UNCOMPRESSED);

        sha3.UseKeccakPadding = true;
        publicKeyBytes = sha3.ComputeHash(serializedUncompressedPublicKey[1..]);

        var last20Bytes = publicKeyBytes[^20..];
        var noChecksumAddress = Convert.ToHexString(last20Bytes).ToLower();

        sha3.Initialize();
        sha3.UseKeccakPadding = true;
        var checksumLast20 = sha3.ComputeHash(Encoding.ASCII.GetBytes(noChecksumAddress));

        var comparingArray = Utilities.HexStringToNumericArray(Convert.ToHexString(checksumLast20));
        var checksumedResult = new StringBuilder(noChecksumAddress.Length);

        for (var i = 0; i < noChecksumAddress.Length; i++)
        {
            checksumedResult.Append(comparingArray[i] >= 8
                ? char.ToUpper(noChecksumAddress[i])
                : char.ToLower(noChecksumAddress[i]));
        }
        return checksumedResult.ToString();
    }

}