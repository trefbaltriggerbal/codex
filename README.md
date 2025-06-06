# codex
This repository contains a simple Linear Congruential Generator (LCG) sample
written in C#. The `HelloWorldApp` project exposes a small command line
interface.

Running the app without arguments starts an interactive CLI. Useful commands
include:

```
short            # generate one random value
fast <n>         # jump ahead n steps using the fast algorithm
set m|a|c <val>  # set multiplier, addition or modulus
get m|a|c|seed   # display current parameters
seed <val>       # set the seed value
exit             # quit the CLI
```

By default the ANSI C parameters are used: multiplier `1103515245`, addition
`12345` and modulus `2^31`.
