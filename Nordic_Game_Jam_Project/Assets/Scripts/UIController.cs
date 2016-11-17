using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject cursorNormal;
    public GameObject cursorInteract;
    public GameObject cursorGrabbed;

    public Image overlay;

    void Start() {
        FadeOut();
    }

    public void SetCursorState(CursorState state) {
        switch (state) {
            case CursorState.NORMAL:
                cursorNormal.SetActive(true);
                cursorInteract.SetActive(false);
                cursorGrabbed.SetActive(false);
                break;

            case CursorState.INTERACT:
                cursorNormal.SetActive(false);
                cursorInteract.SetActive(true);
                cursorGrabbed.SetActive(false);
                break;

            case CursorState.GRABBED:
                cursorNormal.SetActive(false);
                cursorInteract.SetActive(false);
                cursorGrabbed.SetActive(true);
                break;
        }
    }

    public void FadeIn() {
        StartCoroutine(FadeRoutine(1, 10));
    }

    public void FadeOut() {
        StartCoroutine(FadeRoutine(0, 5));
    }

    private IEnumerator FadeRoutine(float target, float duration) {
        float startTime = Time.time;
        Color startColor = overlay.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, target);

        while (Time.time < startTime + duration) {
            float timeValue = (Time.time - startTime) / duration;
            overlay.color = Color.Lerp(startColor, targetColor, timeValue);
            yield return null;
        }
    }

}

public enum CursorState {
    NORMAL,
    INTERACT,
    GRABBED
}