using System.Text;
using DZen.Security.Cryptography;
using Secp256k1Net;
using ETHVanitySharp.Utilities;

namespace TestProject1;

public class Tests
{
    private Secp256k1 secp256k1;
    [SetUp]
    public void Setup()
    {
        secp256k1 = new Secp256k1();
    }

    [Test]
    public void PrivateToAddress_ShouldReturnValidAddress()
    {
        var result =  Utilities.PrivateToAddress("9E02AD4AB4CE21C3A32E06A0AADB873562E1A8E45BD335C076194DCEE55536A6",
           Convert.FromHexString("9E02AD4AB4CE21C3A32E06A0AADB873562E1A8E45BD335C076194DCEE55536A6") ,ref secp256k1);
        Assert.That(result, Is.EqualTo("a60F412DE409dfC3f360EB8C5566EF09D5F8bd0B"));
    }
}