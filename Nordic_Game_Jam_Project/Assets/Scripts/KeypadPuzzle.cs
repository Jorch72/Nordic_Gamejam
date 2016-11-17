using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class KeypadPuzzle : MonoBehaviour {

    public string code = "5481";
    public UnityEvent onComplete;

    public string lastInput = "";
    private bool isCompleted = false;

    public void AddInput(string s) {
        if (isCompleted) return;

        if (lastInput.Length == 4)
            lastInput = lastInput.Substring(1, 3) + s;
        else
            lastInput += s;

        if (lastInput.Equals(code)) {
            onComplete.Invoke();
            isCompleted = true;
        }

        GetComponent<AudioSource>().Play();
    }
}
