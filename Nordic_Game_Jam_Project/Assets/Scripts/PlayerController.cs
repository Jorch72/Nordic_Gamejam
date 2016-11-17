using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

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
    public float grabDistanceMin = 1;
    public float grabDistanceMax = 3;
    public float grabPower = 5;

    public float GrabDistance {
        get { return currentGrabDistance; }
        set { currentGrabDistance = Mathf.Clamp(value, grabDistanceMin, grabDistanceMax); }
    }

    private float currentGrabDistance = 1;
    private Interactable currentlyInteractingWith = null;

    void Awake() {
        instance = this;

        Cursor.visible = false;
    }

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

            float newLookY = cam.transform.localEulerAngles.x - lookInput.y;
            if(newLookY < 360 && newLookY > 200) {
                if (newLookY < 270) newLookY = 270;
            }else if(newLookY > 0 && newLookY < 100) {
                if (newLookY > 90) newLookY = 90;
            }
            cam.transform.localEulerAngles = new Vector3(newLookY, 0, 0);
        }
        body.MovePosition(body.position +
            (transform.forward * moveInput.y * currentMoveSpeed * Time.deltaTime) +
            (transform.right * moveInput.x * currentMoveSpeed * Time.deltaTime));
    }

    private bool CheckRaycast() {
        if(currentlyInteractingWith != null) {
            if (Input.GetMouseButtonUp(0)) {
                currentlyInteractingWith.Reset();
                currentlyInteractingWith = null;
                return false;
            }
            currentlyInteractingWith.Interact();
            uiController.SetCursorState(CursorState.GRABBED);
            return currentlyInteractingWith.blockMouseMovement;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, interactRange, interactLayer)) {
            Interactable iObj = hitInfo.collider.gameObject.GetComponent<Interactable>();
            if (iObj == null || iObj.blockInteract) return false;

            uiController.SetCursorState(CursorState.INTERACT);
            if (Input.GetMouseButton(0)) {
                currentlyInteractingWith = iObj;
            }
            return false;
        } else {
            uiController.SetCursorState(CursorState.NORMAL);
            return false;
        }
    }

    public void ReleaseObject() {
        currentlyInteractingWith = null;
        uiController.SetCursorState(CursorState.NORMAL);
    }
}

