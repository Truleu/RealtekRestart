using System.Diagnostics;


var OSEnv = CheckOSEnvironment();

DisableRealtek(OSEnv);

EnableRealtek(OSEnv);

static string CheckOSEnvironment()
{
  return Environment.OSVersion.ToString();
}

static void DisableRealtek(string OSEnv)
{
  if (string.IsNullOrEmpty(OSEnv) || !OSEnv.ToLower().Contains("windows"))
  {
    Console.WriteLine($"Sistema Operacional incompatível");
    return;
  }
  else
  {
    try
    {
      File.GetAttributes($@"{Environment.CurrentDirectory}\disable.ps1");
      string commandTxt = Path.Combine(Environment.CurrentDirectory, "disable.ps1");


      var startInfo = new ProcessStartInfo
      {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        WorkingDirectory = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\PowerShell\7\",
        FileName = "pwsh.exe",
        Arguments = $@"-Command {commandTxt}"
      };

      var process = new Process()
      {
        StartInfo = startInfo,
      };

      process.Start();
      EventLog.WriteEntry("Realtek Restart Disable", $"{process.StandardOutput.ReadToEnd()} -- {process.StandardError.ReadToEnd()}".Trim());
      Console.WriteLine($"{process.StandardOutput.ReadToEnd()} -- {process.StandardError.ReadToEnd()}".Trim());
      Console.WriteLine("Realtek desabilitado");

      process.WaitForExit();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}

static void EnableRealtek(string OSEnv)
{
  if (string.IsNullOrEmpty(OSEnv) || !OSEnv.ToLower().Contains("windows"))
  {
    Console.WriteLine($"Sistema Operacional incompatível");
    return;
  }
  else
  {
    try
    {
      File.GetAttributes($@"{Environment.CurrentDirectory}\enable.ps1");
      string commandTxt = Path.Combine(Environment.CurrentDirectory, "enable.ps1");


      var startInfo = new ProcessStartInfo
      {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        WorkingDirectory = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\PowerShell\7\",
        FileName = "pwsh.exe",
        Arguments = $@"-Command {commandTxt}"
      };

      var process = new Process()
      {
        StartInfo = startInfo,
      };

      process.Start();
      EventLog.WriteEntry("Realtek Restart Enable", $"{process.StandardOutput.ReadToEnd()} -- {process.StandardError.ReadToEnd()}".Trim());
      Console.WriteLine($"{process.StandardOutput.ReadToEnd()} -- {process.StandardError.ReadToEnd()}".Trim());
      Console.WriteLine("Realtek reabilitado");

      process.WaitForExit();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}

