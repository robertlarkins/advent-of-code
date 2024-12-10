using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day08ResonantCollinearity;

public class Year2024Day08Part02Solver
{
    private Dictionary<char, List<GridPoint>> antennas = [];
    private int gridHeight;
    private int gridWidth;
    private List<string> input;
    
    public Year2024Day08Part02Solver(IEnumerable<string> input)
    {
        this.input = input.ToList();
        gridHeight = this.input.Count;
        gridWidth = this.input[0].Length;

        ParseInput();
    }

    public int Solve()
    {
        var antinodeLocations = new HashSet<GridPoint>();

        foreach (var frequencyAntennas in antennas.Values)
        {
            // Go through every pair of points in frequencyAntennas
            for (var ant1 = 0; ant1 < frequencyAntennas.Count - 1; ant1++)
            {
                for (var ant2 = ant1 + 1; ant2 < frequencyAntennas.Count; ant2++)
                {
                    var antenna1 = frequencyAntennas[ant1];
                    var antenna2 = frequencyAntennas[ant2];
                    
                    var antinodes = CalculateAntinodeLocations(antenna1, antenna2);
                    
                    antinodeLocations.UnionWith(antinodes);
                }   
            }
        }
        
        return antinodeLocations.Count;
    }

    private List<GridPoint> CalculateAntinodeLocations(
        GridPoint antenna1,
        GridPoint antenna2)
    {
        var colDiff = antenna1.Col - antenna2.Col;
        var rowDiff = antenna1.Row - antenna2.Row;

        var isAntinodeDirectionAValid = true;
        var isAntinodeDirectionBValid = true;
        var dist = 0;
        var antinodeLocations = new List<GridPoint>();

        while (isAntinodeDirectionAValid || isAntinodeDirectionBValid)
        {
            if (isAntinodeDirectionAValid)
            {
                var antinodeA = new GridPoint(antenna1.Row + dist * rowDiff, antenna1.Col + dist * colDiff);
                if (IsGridPointWithinGrid(antinodeA))
                {
                    antinodeLocations.Add(antinodeA);
                }
                else
                {
                    isAntinodeDirectionAValid = false;
                }
            }
            
            if (isAntinodeDirectionBValid)
            {
                var antinodeB = new GridPoint(antenna1.Row - dist * rowDiff, antenna1.Col - dist * colDiff);
                if (IsGridPointWithinGrid(antinodeB))
                {
                    antinodeLocations.Add(antinodeB);
                }
                else
                {
                    isAntinodeDirectionBValid = false;
                }
            }

            dist++;
        }
        
        return antinodeLocations;
    }

    private bool IsGridPointWithinGrid(GridPoint gridPoint)
    {
        var isOnValidRow = gridPoint.Row >= 0 &&
                           gridPoint.Row < gridHeight;
        var isOnValidCol = gridPoint.Col >= 0 &&
                           gridPoint.Col < gridWidth;
        
        return isOnValidRow && isOnValidCol;
    }
    
    private void ParseInput()
    {
        var rows = input.ToList();

        for (var row = 0; row < gridHeight; row++)
        {
            var rowValues = rows[row];
            
            for (var col = 0; col < gridWidth; col++)
            {
                var positionValue = rowValues[col]; 
                
                if (positionValue == '.')
                {
                    continue;
                }
                
                var position = new GridPoint(row, col);

                if (!antennas.TryAdd(positionValue, [position]))
                {
                    antennas[positionValue].Add(position);
                }
            }
        }
    }
}
