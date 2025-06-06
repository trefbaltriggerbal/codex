using System;
using System.Linq;
using System.Text;
using LinearCongruentGenerator;

namespace LinearCongruentGenerator.CLI;

public static class CliApp
{
    public static int Run(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

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
                    ShowUsage();
                    return 0;
                default:
                    Console.WriteLine($"Unknown option: {args[i]}");
                    return 1;
            }
        }

        CommandHandler handler;
        try
        {
            handler = new CommandHandler(multiplier, addition, modulus, seed);
        }
        catch (LCGException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }

        if (interactive)
            RunInteractive(handler);
        else
            RunBatch(handler, count);

        return 0;
    }

    private static void RunInteractive(CommandHandler handler)
    {
        Console.WriteLine("Interactive LCG CLI. Type 'help' for commands, 'exit' to quit.");
        while (true)
        {
            Console.Write("LCG> ");
            var line = Console.ReadLine();
            if (line == null)
                break;

            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (!handler.Execute(parts))
                    break;
            }
            catch (LCGException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void RunBatch(CommandHandler handler, int count)
    {
        for (int i = 0; i < count; i++)
            Console.WriteLine(handler.Next());
    }

    private static void ShowUsage()
    {
        Console.WriteLine("Usage: dotnet run -- [options]");
        Console.WriteLine("Options:");
        Console.WriteLine("  -a|--multiplier <value>  Multiplier");
        Console.WriteLine("  -c|--addition <value>    Addition term");
        Console.WriteLine("  -m|--modulus <value>     Modulus");
        Console.WriteLine("  -s|--seed <value>        Seed (default: 1)");
        Console.WriteLine("  -n|--count <value>       Number of values to generate (default: 1)");
        Console.WriteLine("  --cli                    Start interactive CLI");
    }
}
