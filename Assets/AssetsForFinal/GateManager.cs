using UnityEngine;

public class GateManager : MonoBehaviour
{
    [Header("Gates (assign in order)")]
    public LocomotionGate[] gates;

    [Header("Session Info")]
    public string locomotionType = "Continuous";

    private LapTimer _timer;
    private ResultLogger _logger;
    private bool _sessionActive = false;
    private int _gatesPassed = 0;

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
            _gatesPassed = 0;
            _timer.StartTimer();
            Debug.Log($"[Gates] Timer started at gate 0");
        }

        if (_sessionActive)
        {
            _gatesPassed++;
            Debug.Log($"[Gates] Gate {index} passed — total passed: {_gatesPassed}");
        }

        if (isLast && _sessionActive)
        {
            _sessionActive = false;
            float elapsed = _timer.StopTimer();
            _logger.LogResult(locomotionType, gates.Length, _gatesPassed, elapsed);
        }
    }

    public void ResetAll()
    {
        foreach (var g in gates) g.ResetGate();
        _timer.ResetTimer();
        _sessionActive = false;
        _gatesPassed = 0;
    }
}