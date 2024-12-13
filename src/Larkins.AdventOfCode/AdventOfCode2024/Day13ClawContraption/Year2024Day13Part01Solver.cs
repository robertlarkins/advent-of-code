using System.Text.RegularExpressions;
using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day13ClawContraption;

public class Year2024Day13Part01Solver
{
    private List<ClawGame> clawGames = [];
    private const double Tolerance = 1e-12;

    public Year2024Day13Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        return clawGames.Sum(ProcessClawGame);
    }

    private int ProcessClawGame(ClawGame clawGame)
    {
        double prizeX = clawGame.Prize.X;
        double prizeY = clawGame.Prize.Y;
        double aX = clawGame.ButtonA.X;
        double aY = clawGame.ButtonA.Y;
        double bX = clawGame.ButtonB.X;
        double bY = clawGame.ButtonB.Y;

        var n = (prizeX / aX - prizeY / aY) / (bX / aX - bY / aY);
        var m = (prizeX - n * bX) / aX;

        var isMInt = m.TryConvertToInt(Tolerance, out var mInt);
        var isNInt = n.TryConvertToInt(Tolerance, out var nInt);

        if (!isMInt || !isNInt)
        {
            return 0;
        }

        var validX = (mInt * aX + nInt * bX).IsEqualTo(prizeX, Tolerance);
        var validY = (mInt * aY + nInt * bY).IsEqualTo(prizeY, Tolerance);

        if (!validX || !validY)
        {
            return 0;
        }

        return mInt * 3 + nInt;
    }

    private void ParseInput(List<string> input)
    {
        var lines = input.Count;

        var buttonPattern = @"X\+(?<xnum>\d+), Y\+(?<ynum>\d+)";
        var prizePattern = @"X=(?<xnum>\d+), Y=(?<ynum>\d+)";

        for (var i = 0; i < lines; i += 4)
        {
            var buttonA = Regex.Match(input[i], buttonPattern);
            var buttonB = Regex.Match(input[i+1], buttonPattern);
            var prize = Regex.Match(input[i+2], prizePattern);

            var clawGame = new ClawGame
            {
                ButtonA = (int.Parse(buttonA.Groups["xnum"].Value), int.Parse(buttonA.Groups["ynum"].Value)),
                ButtonB = (int.Parse(buttonB.Groups["xnum"].Value), int.Parse(buttonB.Groups["ynum"].Value)),
                Prize = (int.Parse(prize.Groups["xnum"].Value), int.Parse(prize.Groups["ynum"].Value)),
            };

            clawGames.Add(clawGame);
        }
    }

    private class ClawGame
    {
        public (int X, int Y) ButtonA { get; set; }
        public (int X, int Y) ButtonB { get; set; }
        public (int X, int Y) Prize { get; set; }
    }
}
