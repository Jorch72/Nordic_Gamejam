using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserRenderer : MonoBehaviour {

    private LineRenderer lRenderer;

    public Transform laserOrigin;
    public LayerMask aimableLayers;
    public Color laserColor;
    private PlayerController player;

    void Start() {
        lRenderer = GetComponent<LineRenderer>();
        player = PlayerController.Instance;

        lRenderer.material.SetColor("_TintColor", laserColor);
    }

    void FixedUpdate() {

        Vector3 target;

        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, transform.forward, out hit, 10000, aimableLayers)) {
            target = hit.point;
        } else {
            target = laserOrigin.position + (transform.forward * 10000);
        }

        lRenderer.SetPositions(new Vector3[2] { laserOrigin.position, target });
    }
}
