using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InteractableGeneric : InteractComponent {

    public UnityEvent onInteract;
    public bool interactOnce = false;
    private bool hasInteracted = false;

    public override void Interact() {
        if (hasInteracted && hasInteracted) return;
        hasInteracted = true;
        onInteract.Invoke();
    }

    public override void Reset() {
        base.Reset();

        if (interactOnce) {
            hasInteracted = false;
        }
    }
}
