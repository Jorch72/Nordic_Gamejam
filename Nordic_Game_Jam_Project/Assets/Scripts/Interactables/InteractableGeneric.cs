using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InteractableGeneric : InteractComponent {

    public UnityEvent onInteract;

    public override void Interact() {
        onInteract.Invoke();
    }
}
