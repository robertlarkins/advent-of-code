namespace Larkins.AdventOfCode.AdventOfCode2024.Day17ChronospatialComputer;

public class Year2024Day17Part02Solver
{
    private ulong registerA;
    private ulong registerB;
    private ulong registerC;
    private ulong originalRegisterA;
    private ulong originalRegisterB;
    private ulong originalRegisterC;
    private List<int> program = [];
    private List<int> programInReverse = [];
    private int instructionPointer;
    private List<int> output = [];
    private List<ulong> RegisterARegister = [];

    public Year2024Day17Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public ulong Solve()
    {
        programInReverse = Enumerable.Reverse(program).ToList();

        RecurseThrough(0, 0UL);

        return RegisterARegister.Order().First();
    }

    private void RecurseThrough(
        int position,
        ulong currentRegisterA)
    {
        if (position == programInReverse.Count)
        {
            RegisterARegister.Add(currentRegisterA);
            return;
        }

        // go through each octal
        for (var octal = 0UL; octal < 8; octal++)
        {
            // append octal onto currentRegisterA
            var tempRegisterA = currentRegisterA;
            tempRegisterA <<= 3;
            tempRegisterA |= octal;

            var result = GenerateOutput(tempRegisterA);

            if (position >= result.Count)
            {
                continue;
            }

            result.Reverse();

            var isCurrentResultEqualToProgram = result.SequenceEqual(programInReverse.Take(result.Count));

            if (isCurrentResultEqualToProgram)
            {
                RecurseThrough(position + 1, tempRegisterA);
            }
        }
    }

    private List<int> GenerateOutput(ulong registerATestValue)
    {
        registerA = registerATestValue;
        registerB = originalRegisterB;
        registerC = originalRegisterC;
        instructionPointer = 0;
        output = [];

        for (; instructionPointer < program.Count; instructionPointer += 2)
        {
            var opcode = program[instructionPointer];
            var operand = program[instructionPointer + 1];

            var instructionOperation = GetInstructionOperation(opcode);
            instructionOperation(operand);
        }

        return output;
    }

    private ulong GetComboOperandValue(int operand)
    {
        if (operand is >= 0 and <= 3)
        {
            return (ulong)operand;
        }

        return operand switch
        {
            4 => registerA,
            5 => registerB,
            6 => registerC,
            _ => throw new ArgumentException($"Invalid operand {operand}")
        };
    }

    private Action<int> GetInstructionOperation(int opcode)
    {
        return opcode switch
        {
            0 => AdvInstruction,
            1 => BxlInstruction,
            2 => BstInstruction,
            3 => JnzInstruction,
            4 => BxcInstruction,
            5 => OutInstruction,
            6 => BdvInstruction,
            7 => CdvInstruction,
            _ => throw new ArgumentOutOfRangeException(nameof(opcode), opcode, null)
        };
    }

    /// <summary>
    /// The adv instruction (opcode 0) performs division. The numerator is the value in the A register.
    /// The denominator is found by raising 2 to the power of the instruction's combo operand.
    /// (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
    /// The result of the division operation is truncated to an integer and then written to the A register.
    /// This operation is the same as a right bit shift by the comboOperand
    /// </summary>
    private void AdvInstruction(int operand)
    {
        var comboOperandValue = GetComboOperandValue(operand);
        registerA >>= (int)comboOperandValue;
    }

    /// <summary>
    /// The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register.
    /// (The numerator is still read from the A register.)
    /// </summary>
    private void BdvInstruction(int operand)
    {
        var comboOperandValue = GetComboOperandValue(operand);
        registerB = registerA >> (int)comboOperandValue;
    }

    /// <summary>
    /// The cdv instruction (opcode 7) works exactly like the adv instruction except that the result
    /// is stored in the C register. (The numerator is still read from the A register.)
    /// </summary>
    private void CdvInstruction(int operand)
    {
        var comboOperandValue = GetComboOperandValue(operand);
        registerC = registerA >> (int)comboOperandValue;
    }

    /// <summary>
    /// The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the
    /// instruction's literal operand, then stores the result in register B.
    /// </summary>
    /// <param name="literalOperandValue"></param>
    private void BxlInstruction(int literalOperandValue)
    {
        registerB ^= (ulong)literalOperandValue;
    }

    /// <summary>
    /// The bst instruction (opcode 2) calculates the value of its combo operand modulo 8
    /// (thereby keeping only its lowest 3 bits), then writes that value to the B register.
    /// This is the same as a bitwise AND with 7 (111).
    /// </summary>
    private void BstInstruction(int operand)
    {
        var comboOperandValue = GetComboOperandValue(operand);
        registerB = comboOperandValue & 7;
    }

    /// <summary>
    /// The jnz instruction (opcode 3) does nothing if the A register is 0.
    /// However, if the A register is not zero, it jumps by setting the instruction pointer to the
    /// value of its literal operand;
    /// if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
    /// </summary>
    /// <param name="literalOperandValue"></param>
    private void JnzInstruction(int literalOperandValue)
    {
        if (registerA == 0)
        {
            return;
        }

        instructionPointer = literalOperandValue - 2; // the -2 accounts for the instructionPointer += 2 in the for loop
    }

    /// <summary>
    /// The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C,
    /// then stores the result in register B.
    /// (For legacy reasons, this instruction reads an operand but ignores it.)
    /// </summary>
    /// <param name="operandValue"></param>
    private void BxcInstruction(int operandValue)
    {
        registerB ^= registerC;
    }

    /// <summary>
    /// The out instruction (opcode 5) calculates the value of its combo operand modulo 8,
    /// then outputs that value. (If a program outputs multiple values, they are separated by commas.)
    /// modulo 8 is the same as a bitwise and with 7.
    /// </summary>
    private void OutInstruction(int operand)
    {
        var comboOperandValue = GetComboOperandValue(operand);
        output.Add((int)(comboOperandValue & 7));
    }

    private void ParseInput(List<string> input)
    {
        originalRegisterA = ParseRegisterLine(input[0]);
        originalRegisterB = ParseRegisterLine(input[1]);
        originalRegisterC = ParseRegisterLine(input[2]);

        program = ParseProgramLine(input[4]);

        return;

        static ulong ParseRegisterLine(string line) =>
            ulong.Parse(line.Split(':', StringSplitOptions.TrimEntries)[1]);

        static List<int> ParseProgramLine(string line)
        {
            var programEntries = line.Split(':', StringSplitOptions.TrimEntries)[1];

            return programEntries.Split(',').Select(int.Parse).ToList();
        }
    }
}
