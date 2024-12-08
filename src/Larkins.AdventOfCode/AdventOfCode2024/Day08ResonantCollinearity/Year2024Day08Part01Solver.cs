using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day08ResonantCollinearity;

public class Year2024Day08Part01Solver
{
    private Dictionary<char, List<GridPoint>> antennas = [];
    private int mapHeight;
    private int mapWidth;
    private List<string> input;
    
    public Year2024Day08Part01Solver(IEnumerable<string> input)
    {
        this.input = input.ToList();
        mapHeight = this.input.Count;
        mapWidth = this.input[0].Length;

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
        
        var row1 = antenna1.Row + rowDiff;
        var row2 = antenna2.Row - rowDiff;
        var col1 = antenna1.Col + colDiff;
        var col2 = antenna2.Col - colDiff;

        var antinodeLocations = new List<GridPoint>();
        
        if (row1 >= 0 && row1 < mapHeight && col1 >= 0 && col1 < mapWidth)
        {
            antinodeLocations.Add(new GridPoint(row1, col1));
        }
        
        if (row2 >= 0 && row2 < mapHeight && col2 >= 0 && col2 < mapWidth)
        {
            antinodeLocations.Add(new GridPoint(row2, col2));
        }
        
        return antinodeLocations;
    }

    private void ParseInput()
    {
        var rows = input.ToList();

        for (var row = 0; row < mapHeight; row++)
        {
            var rowValues = rows[row];
            
            for (var col = 0; col < mapWidth; col++)
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
