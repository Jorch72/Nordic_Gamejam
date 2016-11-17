using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Socket : MonoBehaviour {

    public bool isSocketed;
    public SocketableObject toSocket;
    public Collider toSocketCollider;
    private Collider thisCollider;

    public UnityEvent onComplete;

    void Start() {
        thisCollider = GetComponent<Collider>();
    }

    void Update() {
        if (isSocketed) {
            return;
        }

        if (toSocketCollider.bounds.Intersects(thisCollider.bounds)) {
            isSocketed = true;
            toSocket.GetComponent<Interactable>().blockInteract = true;

            PlayerController.Instance.ReleaseObject();

            toSocket.transform.position = transform.position;
            toSocket.transform.eulerAngles = transform.eulerAngles;
            toSocketCollider.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void CheckComplete() {
        if (!isSocketed) return;
        onComplete.Invoke();
    }
}
