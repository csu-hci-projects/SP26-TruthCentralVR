using UnityEngine;
using System.IO;
using System;

public class ResultLogger : MonoBehaviour
{
    [Header("Output Options")]
    public bool logToConsole = true;
    public bool logToCSV = true;

    [Header("Danger Zone")]
    public bool deleteCSVOnStart = false;

    private string _csvPath;

    void Awake()
    {
        _csvPath = Path.Combine(Application.persistentDataPath, "locomotion_results.csv");
        Debug.Log($"[Logger] CSV path: {_csvPath}");

        if (deleteCSVOnStart)
        {
            DeleteCSV();
            deleteCSVOnStart = false;
        }

        if (logToCSV && !File.Exists(_csvPath))
            File.WriteAllText(_csvPath, "Timestamp,LocomotionType,GatesTotal,GatesPassed,TimeSecs\n");
    }

    public void LogResult(string locomotionType, int totalGates, int gatesPassed, float timeSecs)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"{timestamp},{locomotionType},{totalGates},{gatesPassed},{timeSecs:F3}";

        if (logToConsole)
            Debug.Log($"[Results] {timestamp} | Type: {locomotionType} | Gates: {gatesPassed}/{totalGates} | Time: {timeSecs:F2}s");

        if (logToCSV)
        {
            File.AppendAllText(_csvPath, line + "\n");
            Debug.Log($"[Results] CSV saved to: {_csvPath}");
        }
    }

    public void DeleteCSV()
    {
        if (File.Exists(_csvPath))
        {
            File.Delete(_csvPath);
            Debug.Log("[Logger] CSV deleted successfully");
        }
        else
        {
            Debug.Log("[Logger] No CSV file found to delete");
        }
    }
}