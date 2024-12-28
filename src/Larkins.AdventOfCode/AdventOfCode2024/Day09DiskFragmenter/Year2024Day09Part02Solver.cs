namespace Larkins.AdventOfCode.AdventOfCode2024.Day09DiskFragmenter;

public class Year2024Day09Part02Solver
{
    private readonly List<DiskBlock> diskMap = [];

    public Year2024Day09Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var currentId = diskMap[^1].Id;

        while (currentId > 0)
        {
            var i = diskMap.FindIndex(block => block.Id == currentId);

            var fileLength = diskMap[i].FileLength;

            for (var j = 0; j < i; j++)
            {
                var freeSpace = diskMap[j].FreeSpaceLength;
                if (freeSpace < fileLength)
                {
                    continue;
                }

                if (j == i - 1)
                {
                    var currentFreeSpace = diskMap[i].FreeSpaceLength;

                    var blockToShift = diskMap[i] with { FreeSpaceLength = freeSpace - fileLength + currentFreeSpace };

                    diskMap[j] = diskMap[j] with { FreeSpaceLength = 0 };

                    diskMap.RemoveAt(i);
                    diskMap.Insert(j + 1, blockToShift);
                }
                else
                {
                    var blockToShift = diskMap[i] with { FreeSpaceLength = freeSpace - fileLength };

                    diskMap[j] = diskMap[j] with { FreeSpaceLength = 0 };

                    var newFreeSpace = diskMap[i - 1].FreeSpaceLength + diskMap[i].TotalLength;

                    diskMap[i - 1] = diskMap[i - 1] with { FreeSpaceLength = newFreeSpace };
                    diskMap.RemoveAt(i);
                    diskMap.Insert(j + 1, blockToShift);
                }

                break;
            }

            currentId--;
        }

        return CalculateChecksum();
    }

    private long CalculateChecksum()
    {
        var checksum = 0L;
        var startPosition = 0;

        foreach (var diskBlock in diskMap)
        {
            checksum += diskBlock.CalculateChecksum(startPosition);
            startPosition += diskBlock.TotalLength;
        }

        return checksum;
    }

    private void ParseInput(string input)
    {
        // This is included to deal with file read including whitespace, such as a new line.
        input = input.Trim();

        for (var i = 0; i < input.Length; i += 2)
        {
            var id = i / 2;
            var fileLength = (int)char.GetNumericValue(input[i]);
            var freeSpaceLength = 0;

            // There is an odd number of digits. The last digit is just file length.
            // No need to include free space.
            if (i < input.Length - 1)
            {
                freeSpaceLength = (int)char.GetNumericValue(input[i + 1]);
            }

            var file = new DiskBlock(id, fileLength, freeSpaceLength);

            diskMap.Add(file);
        }
    }

    private record DiskBlock(
        int Id,
        int FileLength,
        int FreeSpaceLength)
    {
        public bool HasFreeSpace => FreeSpaceLength > 0;

        public int TotalLength => FileLength + FreeSpaceLength;

        public long CalculateChecksum(int startPosition)
        {
            var checksum = 0L;

            for (var i = startPosition; i < startPosition + FileLength; i++)
            {
                checksum += Id * i;
            }

            return checksum;
        }
    }
}
