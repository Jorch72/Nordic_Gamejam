using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserController : MonoBehaviour {

    public Transform laserGoalObject;
    public List<LaserPuzzleGoal> targets;
    private LaserPuzzleGoal currentTarget;

    private bool isMoving = false;

    void Start() {
        currentTarget = targets[1];
        laserGoalObject.position = currentTarget.transform.position;
    }

    [ContextMenu("Next target")]
    public void SelectNextTarget() {
        if (isMoving) return;
        currentTarget.SetTarget(false);

        int count = 0;
        foreach(LaserPuzzleGoal t in targets) {
            if(t == currentTarget) {
                if (targets[targets.Count - 1] == t) currentTarget = targets[0];
                else currentTarget = targets[count + 1];
                break;
            }
            count++;
        }

        StartCoroutine(MoveToNextTarget());
    }

    public IEnumerator MoveToNextTarget() {
        float duration = 2;
        float startTime = Time.time;
        Vector3 startPos = laserGoalObject.position;

        isMoving = true;

        while (Time.time < startTime + duration) {
            float timeValue = (Time.time - startTime) / duration;
            laserGoalObject.position = Vector3.Lerp(startPos, currentTarget.transform.position, timeValue);
            yield return null;
        }

        currentTarget.SetTarget(true);

        isMoving = false;
    }
}
