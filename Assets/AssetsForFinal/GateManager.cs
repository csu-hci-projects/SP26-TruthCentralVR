using UnityEngine;

public class GateManager : MonoBehaviour
{
    [Header("Gates (assign in order)")]
    public LocomotionGate[] gates;

    private LapTimer _timer;
    private ResultLogger _logger;
    private bool _sessionActive = false;

    void Awake()
    {
        _timer  = GetComponent<LapTimer>();
        _logger = GetComponent<ResultLogger>();
    }

    public void OnGatePassed(int index)
    {
        bool isFirst = (index == 0);
        bool isLast  = (index == gates.Length - 1);

        if (isFirst && !_sessionActive)
        {
            _sessionActive = true;
            _timer.StartTimer();
            Debug.Log($"[Gates] Timer started at gate 0");
        }

        Debug.Log($"[Gates] Gate {index} passed");

        if (isLast && _sessionActive)
        {
            _sessionActive = false;
            float elapsed = _timer.StopTimer();
            _logger.LogResult(gates.Length, elapsed);
        }
    }

    public void ResetAll()
    {
        foreach (var g in gates) g.ResetGate();
        _timer.ResetTimer();
        _sessionActive = false;
    }
}