using System;
using LinearCongruentGenerator;

namespace LinearCongruentGenerator.CLI;

public class CommandHandler
{
    private long _multiplier;
    private long _addition;
    private long _modulus;
    private long _seed;
    private LCGRandomizer _rng = null!;

    public CommandHandler(long multiplier, long addition, long modulus, long seed)
    {
        _multiplier = multiplier;
        _addition = addition;
        _modulus = modulus;
        _seed = seed;

        if (!BuildRng(out var err))
            throw new InvalidOperationException($"Invalid initial parameters: {err}");
    }

    public long Next() => _rng.Next();

    public bool Execute(string[] parts)
    {
        if (parts.Length == 0)
            return true;

        switch (parts[0].ToLowerInvariant())
        {
            case "exit":
            case "quit":
                return false;

            case "help":
                Console.WriteLine("Commands:");
                Console.WriteLine("  short                 - output next number");
                Console.WriteLine("  fast <n>              - move n steps (backwards if n<0) and show resulting seed");
                Console.WriteLine("  set m|a|c <value>     - set multiplier/addition/modulus");
                Console.WriteLine("  get m|a|c             - get current multiplier/addition/modulus");
                Console.WriteLine("  seed <value>          - set seed value");
                Console.WriteLine("  get seed              - show current seed");
                Console.WriteLine("  exit                  - quit CLI");
                return true;

            case "short":
                Console.WriteLine(_rng.Next());
                return true;

            case "fast":
                if (parts.Length < 2 || !long.TryParse(parts[1], out var steps))
                {
                    Console.WriteLine("Usage: fast <steps>");
                    return true;
                }
                if (steps == 0)
                {
                    Console.WriteLine(_rng.Seed);
                }
                else
                {
                    try
                    {
                        _rng.Jump(steps);
                        Console.WriteLine(_rng.Seed);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Warning: {ex.Message}. Negative jumps require the multiplier and modulus to be relatively prime.");
                        Console.WriteLine("The jump was not performed.");
                        Console.ResetColor();
                    }
                }
                return true;

            case "set":
                if (parts.Length < 3 || !long.TryParse(parts[2], out var val))
                {
                    Console.WriteLine("Usage: set m|a|c <value>");
                    return true;
                }
                switch (parts[1].ToLowerInvariant())
                {
                    case "m":
                        {
                            var old = _multiplier;
                            _multiplier = val;
                            if (!BuildRng(out var err))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Warning: {err}. Use 'set m {old}' to restore the previous value.");
                                Console.WriteLine("The generator may produce unreliable results until a valid value is provided.");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case "a":
                        {
                            var old = _addition;
                            _addition = val;
                            if (!BuildRng(out var err))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Warning: {err}. Use 'set a {old}' to restore the previous value.");
                                Console.WriteLine("The generator may produce unreliable results until a valid value is provided.");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case "c":
                        {
                            var old = _modulus;
                            _modulus = val;
                            if (!BuildRng(out var err))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Warning: {err}. Use 'set c {old}' to restore the previous value.");
                                Console.WriteLine("The generator may produce unreliable results until a valid value is provided.");
                                Console.ResetColor();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown parameter. Use m, a or c.");
                        break;
                }
                return true;

            case "get":
                if (parts.Length < 2)
                {
                    Console.WriteLine("Usage: get m|a|c|seed");
                    return true;
                }
                switch (parts[1].ToLowerInvariant())
                {
                    case "m":
                        Console.WriteLine(_multiplier);
                        break;
                    case "a":
                        Console.WriteLine(_addition);
                        break;
                    case "c":
                        Console.WriteLine(_modulus);
                        break;
                    case "seed":
                        Console.WriteLine(_rng.Seed);
                        break;
                    default:
                        Console.WriteLine("Unknown parameter. Use m, a, c or seed.");
                        break;
                }
                return true;

            case "seed":
                if (parts.Length < 2 || !long.TryParse(parts[1], out var s))
                {
                    Console.WriteLine("Usage: seed <value>");
                    return true;
                }
                _seed = s;
                _rng.SetSeed(_seed);
                return true;

            default:
                Console.WriteLine("Unknown command. Type 'help' for list of commands.");
                return true;
        }
    }

    private bool BuildRng(out string? error)
    {
        try
        {
            LCGValidator.Validate(_multiplier, _addition, _modulus);
            _rng = new LCGRandomizer(_multiplier, _addition, _modulus, _seed);
            error = null;
            return true;
        }
        catch (LCGException ex)
        {
            error = ex.Message;
            return false;
        }
    }
}
