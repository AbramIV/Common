using Calculator;
using ConsoleApp;
using static System.Console;

ForegroundColor = ConsoleColor.Green;

Guid deviceGuid = new("{36fc9e60-c465-11cf-8056-444553540000}");
string instancePath = @"USB\VID_0C2E&PID_0B61\20144N0013";

try
{
    DeviceHelper.SetDeviceEnabled(deviceGuid, instancePath, true);
}
catch (Exception ex)
{
    ForegroundColor = ConsoleColor.Red;
    WriteLine($"Error: {ex.Message}");
    ForegroundColor = ConsoleColor.Green;
}

WriteLine("\nDone!");

ReadLine();
