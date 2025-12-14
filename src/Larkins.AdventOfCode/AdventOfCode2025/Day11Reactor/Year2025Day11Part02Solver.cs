namespace Larkins.AdventOfCode.AdventOfCode2025.Day11Reactor;

public class Year2025Day11Part02Solver
{
    private readonly Dictionary<string, Device> devices = [];

    public Year2025Day11Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var svr = devices["svr"];
        var fft = devices["fft"];
        var dac = devices["dac"];
        var outD = devices["out"];

        var svrToFft = Path(svr, fft, [dac]);
        ResetToZero();
        var fftToDac = Path(fft, dac, []);
        ResetToZero();
        var dacToOut = Path(dac, outD, [fft]);
        ResetToZero();

        var svrToDac = Path(svr, dac, [fft]);
        ResetToZero();
        var dacToFft = Path(dac, fft, []);
        ResetToZero();
        var fftToOut = Path(fft, outD, [dac]);
        ResetToZero();

        return svrToFft * fftToDac * dacToOut + svrToDac * dacToFft * fftToOut;
    }

    private void ResetToZero()
    {
        foreach (var device in devices.Values)
        {
            device.PathsToDestination = 0;
            device.HasBeenVisited = false;
        }
    }

    private long Path(Device device, Device endDevice, List<Device> ignoreList)
    {
        if (ignoreList.Contains(device))
        {
            return 0;
        }

        if (device == endDevice)
        {
            device.PathsToDestination = 1;
            device.HasBeenVisited = true;
        }

        if (device.HasBeenVisited)
        {
            return device.PathsToDestination;
        }

        foreach (var outputDevice in device.OutputDevices)
        {
            device.PathsToDestination += Path(outputDevice, endDevice, ignoreList);
        }

        device.HasBeenVisited = true;

        return device.PathsToDestination;
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
        public HashSet<Device> OutputDevices { get; } = [];

        public long PathsToDestination { get; set; }

        public bool HasBeenVisited { get; set; }
    }
}
