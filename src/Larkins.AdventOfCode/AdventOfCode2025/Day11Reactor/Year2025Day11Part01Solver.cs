namespace Larkins.AdventOfCode.AdventOfCode2025.Day11Reactor;

public class Year2025Day11Part01Solver
{
    private readonly Dictionary<string, Device> devices = [];
    private int pathCount;

    public Year2025Day11Part01Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        TraverseDevices(devices["you"]);

        return pathCount;
    }

    private void TraverseDevices(Device device)
    {
        if (device.Name == "out")
        {
            pathCount++;
            return;
        }

        foreach (var outputDevice in device.OutputDevices)
        {
            TraverseDevices(outputDevice);
        }
    }

    private void ParseInput(string input)
    {
        var rows = input.Split(Environment.NewLine);

        foreach (var row in rows)
        {
            var currentDevices = row.Split(": ", StringSplitOptions.RemoveEmptyEntries);

            if (!devices.TryGetValue(currentDevices[0], out var currentDevice))
            {
                currentDevice = new Device(currentDevices[0]);
                devices.Add(currentDevices[0], currentDevice);
            }

            var outputDevicesNames = currentDevices[1].Split(" ");

            foreach (var outputDeviceName in outputDevicesNames)
            {
                if (!devices.TryGetValue(outputDeviceName, out var outputDevice))
                {
                    outputDevice = new Device(outputDeviceName);
                    devices.Add(outputDeviceName, outputDevice);
                }

                currentDevice.OutputDevices.Add(outputDevice);
            }
        }
    }

    private record Device(string Name)
    {
        public List<Device> OutputDevices { get; } = [];
    }
}
