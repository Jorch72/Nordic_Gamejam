using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InteractGrabRotate : InteractGrab {

    public Vector3 rotationAxis = new Vector3(0, 0, 1);
    public Vector2 mouseAxis = new Vector2(0, 1);
    public float rotationMin = 0;
    public float rotationMax = 110;

    public float triggerAmount = 90;
    private bool isTriggered = false;
    private bool isDragged = false;

    public UnityEvent onTrigger;

    void Update() {
        if (isDragged) return;
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationMin), 0.2f);
    }

    public override void Interact() {
        base.Interact();

        isDragged = true;

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * mouseAxis.x, Input.GetAxisRaw("Mouse Y") * mouseAxis.y);
        Vector3 goal = transform.localEulerAngles + rotationAxis * (mouseInput.x - mouseInput.y);
        goal = new Vector3(goal.x, goal.y, Mathf.Clamp(goal.z, rotationMin, rotationMax));

        transform.localEulerAngles = goal;

        if (goal.z >= triggerAmount) {
            if (isTriggered) return;
            isTriggered = true;
            onTrigger.Invoke();
        } else {
            isTriggered = false;
        }
    }

    public override void Reset() {
        isDragged = false;
    }
}
