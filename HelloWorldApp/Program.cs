using System.Text;
using LinearCongruentGenerator;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

if (args.Length == 0 || args.Contains("--help"))
{
    Console.WriteLine("Usage: dotnet run -- [options]");
    Console.WriteLine("Options:");
    Console.WriteLine("  -a|--multiplier <value>  Multiplier (required)");
    Console.WriteLine("  -c|--addition <value>    Addition term (required)");
    Console.WriteLine("  -m|--modulus <value>     Modulus (required)");
    Console.WriteLine("  -s|--seed <value>        Seed (default: 1)");
    Console.WriteLine("  -n|--count <value>       Number of values to generate (default: 1)");
    return;
}

long multiplier = 0;
long addition = 0;
long modulus = 0;
long seed = 1;
int count = 1;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "-a":
        case "--multiplier":
            multiplier = long.Parse(args[++i]);
            break;
        case "-c":
        case "--addition":
            addition = long.Parse(args[++i]);
            break;
        case "-m":
        case "--modulus":
            modulus = long.Parse(args[++i]);
            break;
        case "-s":
        case "--seed":
            seed = long.Parse(args[++i]);
            break;
        case "-n":
        case "--count":
            count = int.Parse(args[++i]);
            break;
        default:
            Console.WriteLine($"Unknown option: {args[i]}");
            return;
    }
}

if (multiplier == 0 || modulus == 0)
{
    Console.WriteLine("Multiplier and modulus are required. Use --help for usage.");
    return;
}

var rng = new LCGRandomizer(multiplier, addition, modulus, seed);
for (int i = 0; i < count; i++)
{
    Console.WriteLine(rng.Next());
}
