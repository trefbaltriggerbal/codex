# Issue Breakdown for LinearCongruentGenerator Project

1. **Create Class Library Project**
   - Add a new class library named `LinearCongruentGenerator`.
   - Add project to existing solution `HelloWorldApp.sln`.

2. **Implement `LCGRandomizer`**
   - Methods:
     - `Next()` – returns next random number.
     - `SetSeed(long seed)` – set current seed.
     - `Jump(long jumps)` – use fast jump ahead formula.

3. **Implement `LCGBuilder` with Fluent API**
   - Methods:
     - `SetAddition(long addition)`
     - `SetMultiplier(long multiplier)`
     - `SetModulus(long modulus)`
   - `Build()` constructs `LCGRandomizer` after validation.

4. **Implement `LCGValidator`**
   - `Validate(long multiplier, long addition, long modulus)` called by builder.

5. **(Optional) Add `LCGTester` Helper**
   - Utility for manual tests.

6. **Create Test Project `LinearCongruentGenerator.Tests`**
   - Validate ANSI C constants and reference sequence.
   - Verify `Jump()` matches repeated `Next()` calls.
   - Ensure identical builders yield same sequences.

7. **Update Documentation**
   - Add build/test instructions and usage examples to `README.md`.

Each of these tasks corresponds to a GitHub Issue and later a Pull Request.
