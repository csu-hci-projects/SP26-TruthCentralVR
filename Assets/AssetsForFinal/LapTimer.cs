using UnityEngine;

public class LapTimer : MonoBehaviour
{
    private float _startTime;
    private float _elapsed;
    private bool _running = false;

    public float Elapsed => _running ? Time.time - _startTime : _elapsed;

    public void StartTimer()
    {
        _startTime = Time.time;
        _running = true;
    }

    public float StopTimer()
    {
        _elapsed = Time.time - _startTime;
        _running = false;
        return _elapsed;
    }

    public void ResetTimer()
    {
        _elapsed = 0f;
        _running = false;
    }
}