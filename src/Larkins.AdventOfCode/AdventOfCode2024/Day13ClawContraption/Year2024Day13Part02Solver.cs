using System.Text.RegularExpressions;
using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day13ClawContraption;

public class Year2024Day13Part02Solver
{
    private readonly List<ClawGame> clawGames = [];
    private const decimal Tolerance = 1e-8M;

    public Year2024Day13Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public long Solve()
    {
        return clawGames.Sum(ProcessClawGame);
    }

    private long ProcessClawGame(ClawGame clawGame)
    {
        decimal prizeX = clawGame.Prize.X;
        decimal prizeY = clawGame.Prize.Y;
        decimal aX = clawGame.ButtonA.X;
        decimal aY = clawGame.ButtonA.Y;
        decimal bX = clawGame.ButtonB.X;
        decimal bY = clawGame.ButtonB.Y;

        var n = (prizeX * aY - prizeY * aX) / (bX * aY - bY * aX);
        var m = (prizeX - n * bX) / aX;

        var isMLong = m.TryConvertToLong(Tolerance, out var mLong);
        var isNLong = n.TryConvertToLong(Tolerance, out var nLong);

        if (!isMLong || !isNLong)
        {
            return 0;
        }

        return mLong * 3 + nLong;
    }

    private void ParseInput(List<string> input)
    {
        var lines = input.Count;

        var buttonRegex = Y24D13RegexPatterns.ButtonRegex();
        var prizePattern = Y24D13RegexPatterns.PrizeLocationRegex();

        var prizeLocationShift = 10_000_000_000_000L;

        for (var i = 0; i < lines; i += 4)
        {
            var buttonA = buttonRegex.Match(input[i]);
            var buttonB = buttonRegex.Match(input[i+1]);
            var prize = prizePattern.Match(input[i+2]);

            var clawGame = new ClawGame
            {
                ButtonA = (int.Parse(buttonA.Groups["xnum"].Value), int.Parse(buttonA.Groups["ynum"].Value)),
                ButtonB = (int.Parse(buttonB.Groups["xnum"].Value), int.Parse(buttonB.Groups["ynum"].Value)),
                Prize = (int.Parse(prize.Groups["xnum"].Value) + prizeLocationShift, int.Parse(prize.Groups["ynum"].Value) + prizeLocationShift)
            };

            clawGames.Add(clawGame);
        }
    }

    private class ClawGame
    {
        public (int X, int Y) ButtonA { get; set; }
        public (int X, int Y) ButtonB { get; set; }
        public (long X, long Y) Prize { get; set; }
    }
   
}

public static partial class Y24D13RegexPatterns
{
    [GeneratedRegex(@"X\+(?<xnum>\d+), Y\+(?<ynum>\d+)")]
    public static partial Regex ButtonRegex();

    [GeneratedRegex(@"X=(?<xnum>\d+), Y=(?<ynum>\d+)")]
    public static partial Regex PrizeLocationRegex();
}