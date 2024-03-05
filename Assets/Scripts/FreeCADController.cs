using UnityEngine;
using System.Diagnostics;

public class FreeCADController : MonoBehaviour
{
    public string scriptPath = "C:\\Users\\ne23946\\IntelligentCAD\\GenerateFeature.py";
    public string freecadCmdPath = @"C:\\Program Files\\FreeCAD 0.20\\bin\\FreeCADCmd.exe";

    public void RunFreeCADScript()
    {
        // Construct the command to execute
        string command = $"\"{freecadCmdPath}\" {scriptPath}";

        // Create process start info
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C {command}";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        // Start the process
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Read output and errors
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        // Log output and errors
        UnityEngine.Debug.Log("STDOUT: " + output);
        UnityEngine.Debug.LogError("STDERR: " + error);
    }
}

//using UnityEngine;
//using System.Diagnostics;

//public class FreeCADController : MonoBehaviour
//{
//    public string scriptPath = "C:\\Users\\ne23946\\basicScript.py";
//    public string freecadCmdPath = @"C:\\Program Files\\FreeCAD 0.20\\bin\\FreeCADCmd.exe";
//    private Process process;

//    void Start()
//    {
//        // Start FreeCAD process
//        StartFreeCADProcess();
//    }

//    //void OnDestroy()
//    //{
//    //    // Kill FreeCAD process when the GameObject is destroyed
//    //    if (process != null && !process.HasExited)
//    //    {
//    //        process.Kill();
//    //        process.WaitForExit();
//    //        process.Close();
//    //    }
//    //}

//    void StartFreeCADProcess()
//    {
//        // Construct the command to execute
//        string command = $"\"{freecadCmdPath}\" {scriptPath}";

//        // Create process start info
//        ProcessStartInfo startInfo = new ProcessStartInfo();
//        startInfo.FileName = "cmd.exe";
//        startInfo.Arguments = $"/C {command}";
//        startInfo.RedirectStandardInput = true;  // Ensure standard input redirection is enabled
//        startInfo.RedirectStandardOutput = true;
//        startInfo.RedirectStandardError = true;
//        startInfo.UseShellExecute = false;
//        startInfo.CreateNoWindow = true;

//        // Start the process
//        process = new Process();
//        process.StartInfo = startInfo;
//        process.Start();


//        ////Read output and errors
//        //string output = process.StandardOutput.ReadToEnd();
//        //string error = process.StandardError.ReadToEnd();

//        //// Log output and errors
//        //UnityEngine.Debug.Log("STDOUT: " + output);
//        //UnityEngine.Debug.LogError("STDERR: " + error);
//        UnityEngine.Debug.Log(process);

//        // Wait for FreeCAD to initialize (optional)
//        // You may need to adjust the delay based on your system
//        //System.Threading.Thread.Sleep(5000);
//    }

//    public void RunFreeCADScript()
//    {
//        UnityEngine.Debug.Log(process);
//        UnityEngine.Debug.Log(scriptPath);
//        // Check if the process is running and its StandardInput is redirected
//        if (process != null && process.StartInfo.RedirectStandardInput)
//        {
//            // Send script to FreeCAD process
//            process.StandardInput.WriteLine($"/C {scriptPath}");
//            // Flush the input stream to ensure the command is sent
//            process.StandardInput.Flush();
//        }
//        else
//        {
//            UnityEngine.Debug.LogError("FreeCAD process is not running or StandardInput is not redirected.");
//        }
//    }

//}