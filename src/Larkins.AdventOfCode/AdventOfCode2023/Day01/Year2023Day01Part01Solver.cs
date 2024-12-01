namespace Larkins.AdventOfCode.AdventOfCode2023.Day01;

public class Year2023Day01Part01Solver
{
    public int Solve(IEnumerable<string> input) => input.Sum(ConciseCalibrationValueExtract);

    private int ConciseCalibrationValueExtract(string line)
    {
        var digits = line.Where(char.IsDigit)
            .Select(c => int.Parse(c.ToString()))
            .ToList();
        return digits[0] * 10 + digits[^1];
    }
    
    private int ExtractCalibrationValue(string line)
    {
        var firstDigit = ExtractFirstDigit();
        var lastDigit = ExtractLastDigit();

        return firstDigit * 10 + lastDigit;

        int ExtractFirstDigit()
        {
            var firstDigit1 = -1;
            foreach (var t in line.Where(char.IsDigit))
            {
                firstDigit1 = int.Parse(t.ToString());
                break;
            }

            if (firstDigit1 == -1)
            {
                throw new Exception();
            }

            return firstDigit1;
        }

        int ExtractLastDigit()
        {
            var lastDigit1 = -1;
            for (var i = line.Length -1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    lastDigit1 = int.Parse(line[i].ToString());
                    break;
                }
            }

            if (lastDigit1 == -1)
            {
                throw new Exception();
            }

            return lastDigit1;
        }
    }
}