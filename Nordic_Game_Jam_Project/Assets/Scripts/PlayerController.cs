using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Header("Components")]
    public Camera cam;
    public UIController uiController;
    private Rigidbody body;

    [Header("Settings")]
    public LayerMask interactLayer;
    public float interactRange = 2;
    public float cameraSensitivity = 1;
    public float moveSpeed = 1;
    public float dragSpeed = 1f;

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void Update() {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 lookInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * cameraSensitivity;

        float currentMoveSpeed = moveSpeed;
        if (CheckRaycast()) {
            currentMoveSpeed = dragSpeed;
        } else {
            transform.localEulerAngles += new Vector3(0, lookInput.x, 0);
            cam.transform.localEulerAngles += new Vector3(-lookInput.y, 0, 0);
        }
        body.MovePosition(body.position +
            (transform.forward * moveInput.y * currentMoveSpeed * Time.deltaTime) +
            (transform.right * moveInput.x * currentMoveSpeed * Time.deltaTime));
    }

    private bool CheckRaycast() {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, interactRange, interactLayer)) {
            Interactable iObj = hitInfo.collider.gameObject.GetComponent<Interactable>();
            if (iObj == null) return false;

            uiController.SetCursorState(CursorState.INTERACT);
            if (Input.GetMouseButton(0)) {
                return true;
            }

            return false;
        } else {
            uiController.SetCursorState(CursorState.NORMAL);
            return false;
        }
    }
}

