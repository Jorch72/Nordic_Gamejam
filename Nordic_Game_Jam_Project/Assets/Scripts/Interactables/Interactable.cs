using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public bool blockInteract = false;
    public bool blockMouseMovement = true;
    public InteractComponent[] components;

    public void Interact() {
        foreach(InteractComponent c in components) {
            c.Interact();
        }
    }

    public void Reset() {
        foreach (InteractComponent c in components) {
            c.Reset();
        }
    }

    public void SetBlockInteract(bool b) {
        blockInteract = b;
    }
}
