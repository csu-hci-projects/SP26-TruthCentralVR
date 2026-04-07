using UnityEngine;
using System.IO;
using System;

public class ResultLogger : MonoBehaviour
{
    [Header("Output Options")]
    public bool logToConsole = true;
    public bool logToCSV = true;

    private string _csvPath;

    void Awake()
    {
        _csvPath = Path.Combine(Application.persistentDataPath, "locomotion_results.csv");

        if (logToCSV && !File.Exists(_csvPath))
            File.WriteAllText(_csvPath, "Timestamp,GatesTotal,TimeSecs\n");
    }

    public void LogResult(int totalGates, float timeSecs)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"{timestamp},{totalGates},{timeSecs:F3}";

        if (logToConsole)
            Debug.Log($"[Results] {timestamp} | Gates: {totalGates} | Time: {timeSecs:F2}s");

        if (logToCSV)
        {
            File.AppendAllText(_csvPath, line + "\n");
            Debug.Log($"[Results] CSV saved to: {_csvPath}");
        }
    }
}