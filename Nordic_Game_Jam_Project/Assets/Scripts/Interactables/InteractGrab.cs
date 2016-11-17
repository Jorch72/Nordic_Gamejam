using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class InteractGrab : InteractComponent {

    protected Rigidbody body;
    protected PlayerController player;
    protected Vector3 offset = Vector3.zero;

    private bool isGrabbed = false;

    void Start() {
        body = GetComponent<Rigidbody>();
        player = PlayerController.Instance;
    }


    public override void Interact() {
        base.Interact();

        player.GrabDistance += Input.mouseScrollDelta.y * 0.4f;

        Vector3 offset = player.cam.transform.forward * player.GrabDistance;
        Vector3 goal = player.cam.transform.position + offset + this.offset;

        body.velocity = (goal - body.position) * player.grabPower;

        body.useGravity = false;
    }

    public override void Reset() {
        base.Reset();

        body.useGravity = true;
        body.angularVelocity = Vector3.zero;
    }
}