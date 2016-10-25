using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothDampTime = 0.2f;
    [HideInInspector]
    public new Transform transform;
    public Vector3 cameraOffset;
    public bool useFixedUpdate = false;

    private Vector3 _smoothDampVelocity;


    void Awake()
    {
        transform = gameObject.transform;
    }

    void Start()
    {
        transform.position = target.transform.position;
    }


    void LateUpdate()
    {
        if (!useFixedUpdate)
            updateCameraPosition();
    }


    void FixedUpdate()
    {
        if (useFixedUpdate)
            updateCameraPosition();
    }


    void updateCameraPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

}