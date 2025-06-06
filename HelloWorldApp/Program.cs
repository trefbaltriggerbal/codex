using System.Text;
using LinearCongruentGenerator;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

// ANSI C LCG defaults
long multiplier = 1103515245;
long addition = 12345;
long modulus = 1L << 31; // 2^31
long seed = 1;
int count = 1;

bool interactive = args.Length == 0 || args.Contains("--cli");

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
        case "--cli":
            interactive = true;
            break;
        case "--help":
            Console.WriteLine("Usage: dotnet run -- [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("  -a|--multiplier <value>  Multiplier");
            Console.WriteLine("  -c|--addition <value>    Addition term");
            Console.WriteLine("  -m|--modulus <value>     Modulus");
            Console.WriteLine("  -s|--seed <value>        Seed (default: 1)");
            Console.WriteLine("  -n|--count <value>       Number of values to generate (default: 1)");
            Console.WriteLine("  --cli                    Start interactive CLI");
            return;
        default:
            Console.WriteLine($"Unknown option: {args[i]}");
            return;
    }
}

LCGRandomizer rng = new(multiplier, addition, modulus, seed);

void RebuildRng()
{
    rng = new LCGRandomizer(multiplier, addition, modulus, seed);
}

if (interactive)
{
    Console.WriteLine("Interactive LCG CLI. Type 'help' for commands, 'exit' to quit.");
    while (true)
    {
        Console.Write("LCG> ");
        var line = Console.ReadLine();
        if (line == null)
            break;

        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            continue;

        switch (parts[0].ToLowerInvariant())
        {
            case "exit":
            case "quit":
                return;

            case "help":
                Console.WriteLine("Commands:");
                Console.WriteLine("  short                 - output next number");
                Console.WriteLine("  fast <n>              - move n steps (backwards if n<0) and show resulting seed");
                Console.WriteLine("  set m|a|c <value>     - set multiplier/addition/modulus");
                Console.WriteLine("  get m|a|c             - get current multiplier/addition/modulus");
                Console.WriteLine("  seed <value>          - set seed value");
                Console.WriteLine("  get seed              - show current seed");
                Console.WriteLine("  exit                  - quit CLI");
                break;

            case "short":
                Console.WriteLine(rng.Next());
                break;

            case "fast":
                if (parts.Length < 2 || !long.TryParse(parts[1], out var steps))
                {
                    Console.WriteLine("Usage: fast <steps>");
                    break;
                }
                if (steps == 0)
                {
                    Console.WriteLine(rng.Seed);
                }
                else
                {
                    rng.Jump(steps);
                    Console.WriteLine(rng.Seed);
                }
                break;

            case "set":
                if (parts.Length < 3 || !long.TryParse(parts[2], out var val))
                {
                    Console.WriteLine("Usage: set m|a|c <value>");
                    break;
                }
                switch (parts[1].ToLowerInvariant())
                {
                    case "m":
                        multiplier = val;
                        RebuildRng();
                        break;
                    case "a":
                        addition = val;
                        RebuildRng();
                        break;
                    case "c":
                        modulus = val;
                        RebuildRng();
                        break;
                    default:
                        Console.WriteLine("Unknown parameter. Use m, a or c.");
                        break;
                }
                break;

            case "get":
                if (parts.Length < 2)
                {
                    Console.WriteLine("Usage: get m|a|c|seed");
                    break;
                }
                switch (parts[1].ToLowerInvariant())
                {
                    case "m":
                        Console.WriteLine(multiplier);
                        break;
                    case "a":
                        Console.WriteLine(addition);
                        break;
                    case "c":
                        Console.WriteLine(modulus);
                        break;
                    case "seed":
                        Console.WriteLine(rng.Seed);
                        break;
                    default:
                        Console.WriteLine("Unknown parameter. Use m, a, c or seed.");
                        break;
                }
                break;

            case "seed":
                if (parts.Length < 2 || !long.TryParse(parts[1], out var s))
                {
                    Console.WriteLine("Usage: seed <value>");
                    break;
                }
                seed = s;
                rng.SetSeed(seed);
                break;

            default:
                Console.WriteLine("Unknown command. Type 'help' for list of commands.");
                break;
        }
    }
}
else
{
    for (int i = 0; i < count; i++)
    {
        Console.WriteLine(rng.Next());
    }
}
