using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InteractableGeneric : InteractComponent {

    public UnityEvent events;

    public override void Interact() {
        events.Invoke();
    }
}
