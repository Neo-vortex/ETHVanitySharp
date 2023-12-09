using BenchmarkDotNet.Attributes;
using ETHVanitySharp.Utilities;
using Secp256k1Net;

namespace ETHVanitySharp.Benchmark;

[MemoryDiagnoser]
public class AddressGeneratorBenchmark
{

    [Benchmark]
    public void AddressGenerationAndValidation()
    {
        var  secp256k1 = new Secp256k1();
        var candidatePublicAddress = string.Empty;
        var candidatePrivateKey = string.Empty;
        var randomBytes =  Utilities.Utilities.GetRandomBytes(32);
        candidatePrivateKey = Convert.ToHexString(randomBytes);
        var result =  Utilities.Utilities.PrivateToAddress(candidatePrivateKey,
            randomBytes ,ref secp256k1);
        var final = result.StartsWith("dead") && result.EndsWith("dead");
    }
}