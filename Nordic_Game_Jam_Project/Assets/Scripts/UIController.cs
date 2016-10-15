using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public GameObject cursorNormal;
    public GameObject cursorInteract;

    public void SetCursorState(CursorState state) {
        switch (state) {
            case CursorState.NORMAL:
                cursorNormal.SetActive(true);
                cursorInteract.SetActive(false);
                break;

            case CursorState.INTERACT:
                cursorNormal.SetActive(false);
                cursorInteract.SetActive(true);
                break;
        }
    }

}

public enum CursorState {
    NORMAL,
    INTERACT
}