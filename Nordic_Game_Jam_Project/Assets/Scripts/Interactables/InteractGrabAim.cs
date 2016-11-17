using UnityEngine;
using System.Collections;

public class InteractGrabAim : InteractGrab {

    public bool aimableObject = false;
    public LayerMask aimableLayers;
    public GameObject aimToggleObject;

    private bool isGrabbed = false;

    void Start() {

        body = GetComponent<Rigidbody>();
        player = PlayerController.Instance;

        offset = new Vector3(0, -0.25f, 0);

        if (aimToggleObject != null)
            aimToggleObject.SetActive(false);
    }


    public override void Interact() {
        base.Interact();

        RaycastHit hit;
        if (Physics.Raycast(player.cam.transform.position + (player.cam.transform.forward * (player.GrabDistance + 0.5f)), player.cam.transform.forward, out hit, 10000, aimableLayers)) {
            transform.LookAt(hit.point);

            LaserPuzzleGoal laserPuzzleGoal = hit.collider.GetComponentInParent<LaserPuzzleGoal>();
            if(laserPuzzleGoal != null && Input.GetMouseButton(1)) {
                laserPuzzleGoal.IsShotAt();
            }
        } else {
            transform.localEulerAngles = player.cam.transform.eulerAngles;
        }

        if (aimToggleObject != null)
            aimToggleObject.SetActive(Input.GetMouseButton(1));

    }

    public override void Reset() {
        base.Reset();

        if (aimToggleObject != null)
            aimToggleObject.SetActive(false);
    }
}
