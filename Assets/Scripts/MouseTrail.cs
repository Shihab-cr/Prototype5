using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private Collider collider;
    private Camera mainCamera;
    private float zDepth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        collider = GetComponent<Collider>();
        trailRenderer = GetComponent<TrailRenderer>();

        zDepth = transform.position.z;
        trailRenderer.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trailRenderer.enabled = true;
            collider.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            trailRenderer.enabled = false;
            collider.enabled = false;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateTrailPosition();
        }
    }

    void UpdateTrailPosition()
    {
        Vector3 trailPosition = Input.mousePosition;
        trailPosition.z = 0;
        Vector3 trailWorldPosition = mainCamera.ScreenToWorldPoint(trailPosition);
        trailWorldPosition.z = 0;
        transform.position = trailWorldPosition;
    }
}
