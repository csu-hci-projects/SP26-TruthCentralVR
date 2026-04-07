using UnityEngine;

public class LocomotionGate : MonoBehaviour
{
    [Header("Setup")]
    public int gateIndex;
    public Renderer leftPost;
    public Renderer rightPost;

    [Header("Materials")]
    public Material pendingMaterial;    // red
    public Material passedMaterial;     // green

    private bool _passed = false;
    private GateManager _manager;

    void Start()
    {
        _manager = FindObjectOfType<GateManager>();
        SetState(false);
    }

    // Attach THIS script to TriggerZone, not the parent
    private void OnTriggerEnter(Collider other)
    {
        if (_passed) return;
        if (!other.CompareTag("Player")) return;

        _passed = true;
        SetState(true);
        _manager.OnGatePassed(gateIndex);
    }

    void SetState(bool passed)
    {
        Material mat = passed ? passedMaterial : pendingMaterial;
        if (leftPost)  leftPost.material  = mat;
        if (rightPost) rightPost.material = mat;
    }

    public void ResetGate()
    {
        _passed = false;
        SetState(false);
    }
}