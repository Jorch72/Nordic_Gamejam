using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public InteractComponent[] components;

    public void Interact() {
        foreach(InteractComponent c in components) {
            c.Interact();
        }
    }
}
