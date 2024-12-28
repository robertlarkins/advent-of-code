namespace Larkins.AdventOfCode.AdventOfCode2024.Day09DiskFragmenter;

public class Year2024Day09Part01Solver
{
    private readonly List<DiskBlock> diskMap = [];

    public Year2024Day09Part01Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        // have a pointer to the current file that has freeSpace
        // have a pointer to the last file
        // for each file that is not the last file, fill its free space with blocks from the last file
        var currentPosition = 0;

        while (true)
        {
            if (currentPosition >= diskMap.Count - 1)
            {
                diskMap[currentPosition] = diskMap[currentPosition] with { FreeSpaceLength = 0 };
                break;
            }

            if (!diskMap[currentPosition].HasFreeSpace)
            {
                currentPosition++;
                continue;
            }

            var freeSpace = diskMap[currentPosition].FreeSpaceLength;
            var shiftedBlocks = new List<DiskBlock>();

            while (freeSpace > 0)
            {
                if (currentPosition >= diskMap.Count - 1)
                {
                    break;
                }

                if (currentPosition == diskMap.Count - 2)
                {
                    diskMap[currentPosition] = diskMap[currentPosition] with { FreeSpaceLength = 0 };
                    break;
                }

                if (freeSpace >= diskMap[^1].FileLength)
                {
                    var shiftedBlock = new DiskBlock(diskMap[^1].Id, diskMap[^1].FileLength, 0);
                    shiftedBlocks.Add(shiftedBlock);
                    freeSpace -= diskMap[^1].FileLength;
                    diskMap.RemoveAt(diskMap.Count - 1);
                }
                else
                {
                    var shiftedBlock = new DiskBlock(diskMap[^1].Id, freeSpace, 0);
                    shiftedBlocks.Add(shiftedBlock);
                    diskMap[^1] = diskMap[^1] with { FileLength = diskMap[^1].FileLength - freeSpace };
                    freeSpace = 0;
                }
            }

            diskMap.InsertRange(currentPosition + 1, shiftedBlocks);
            diskMap[currentPosition] = diskMap[currentPosition] with { FreeSpaceLength = 0 };

            currentPosition++;
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
