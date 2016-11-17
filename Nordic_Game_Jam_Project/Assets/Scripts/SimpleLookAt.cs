using UnityEngine;
using System.Collections;

public class SimpleLookAt : MonoBehaviour {

    public Transform lookAt;

	void Update () {
	    if(lookAt == null) {
            return;
        }

        transform.LookAt(lookAt);
	}
}
