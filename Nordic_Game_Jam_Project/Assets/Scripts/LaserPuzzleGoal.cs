using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LaserPuzzleGoal : MonoBehaviour {

    public UnityEvent onTarget;
    public UnityEvent onUntarget;
    public UnityEvent onShotAt;

    private bool isCompleted = false;

    private bool isTargetted = false;

    void Start() {
        onUntarget.Invoke();
    }

    public void SetTarget(bool b) {
        if (b) onTarget.Invoke();
        else onUntarget.Invoke();

        isTargetted = b;
    }

    public void IsShotAt() {
        if (!isTargetted || isCompleted) return;
        isCompleted = true;
        onShotAt.Invoke();
    }
}
