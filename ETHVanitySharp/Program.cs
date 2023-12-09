using System.Diagnostics;
using Secp256k1Net;
using ETHVanitySharp.Utilities;

long count = 0;
var shouldRun = true;
Console.WriteLine("prefix : ");
var prefix = Console.ReadLine();
Console.WriteLine("postfix : ");
var postfix = Console.ReadLine();

var timer = new Stopwatch();
timer.Start();

new Thread(() =>
{
    var timer2 = new Stopwatch();
    timer2.Start();

    var cursorTop = Console.CursorTop;

    while (shouldRun)
    {
        Console.SetCursorPosition(0, cursorTop);

        Console.WriteLine("----------");
        Console.WriteLine("Generated Address : " + count);
        Console.WriteLine("Complexity Size : " + Math.Pow(68,prefix.Length + postfix.Length));
        Console.WriteLine("Speed : " + Math.Round(((double)count / (timer2.ElapsedMilliseconds == 0 ? 1 : timer2.ElapsedMilliseconds)) * 1000, 2)  + " address/sec");
        Console.WriteLine("Probably Covered : " + Math.Round((count / Math.Pow(68,prefix.Length + postfix.Length)) *100 ,1 )  + "%");
        Console.Write(new string(' ', Console.WindowWidth - 1));

        Thread.Sleep(1000);
    }
}).Start();

for (var i = 0; i < Environment.ProcessorCount -1; i++)
{
    new Thread(() =>
    {
        var  secp256k1 = new Secp256k1();
        var candidatePublicAddress = string.Empty;
        var candidatePrivateKey = string.Empty;
        while (!(candidatePublicAddress.StartsWith(prefix!) & candidatePublicAddress.EndsWith(postfix!)  ) && shouldRun )
        {
            var randomBytes =  Utilities.GetRandomBytes(32);
            candidatePrivateKey = Convert.ToHexString(randomBytes);
            candidatePublicAddress = Utilities.PrivateToAddress(candidatePrivateKey, randomBytes,ref secp256k1);
            Interlocked.Increment(ref count);
        }

        if (!shouldRun) return;
        shouldRun = false;
        timer.Stop();
        Console.WriteLine("time took : " + timer.ElapsedMilliseconds/1000.0 + " s");
        Console.WriteLine("address:" + candidatePublicAddress);
        Console.WriteLine("private key:" + candidatePrivateKey);
        Console.ReadLine();

    }).Start();
}

return;

