using UnityEngine;

public class LocomotionGateTeleport : MonoBehaviour
{
    [Header("Setup")]
    public int gateIndex;
    public Renderer leftPost;
    public Renderer rightPost;
    public Transform playerTransform;

    [Header("Detection")]
    public float detectionRadius = 1.5f;

    [Header("Materials")]
    public Material redMaterial;
    public Material greenMaterial;

    private bool _passed = false;
    private GateManagerTeleport _manager;

    void Start()
    {
        _manager = FindObjectOfType<GateManagerTeleport>();
        SetState(false);

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (_passed) return;
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < detectionRadius)
        {
            _passed = true;
            SetState(true);
            _manager.OnGatePassed(gateIndex);
            Debug.Log($"[Gate {gateIndex}] Passed by proximity");
        }
    }

    void SetState(bool passed)
    {
        Material mat = passed ? greenMaterial : redMaterial;
        if (leftPost)  leftPost.material = mat;
        if (rightPost) rightPost.material = mat;
    }

    public void ResetGate()
    {
        _passed = false;
        SetState(false);
    }
}