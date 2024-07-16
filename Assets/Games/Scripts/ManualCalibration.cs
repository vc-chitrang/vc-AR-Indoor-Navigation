using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManualCalibration : MonoBehaviour {

    private Transform rotationPivot;
    private Transform positionPivot;

    private bool isCalibratingRotation;
    private bool isCalibratingPosition;

    private Vector3 initialTouchPosition;
    private Vector3 initialObjectPosition;
    private Vector3 initialObjectRotation;

    public UnityEvent OnCalibrationStart;
    public UnityEvent OnCalibrationEnd;



    private void Awake() {
        rotationPivot = transform;
        positionPivot = transform;
        isCalibratingRotation = false;
        isCalibratingPosition = false;
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void LateUpdate() {
        CalibratePosition();
        CalibrateRotation();
    }

    public void EnableCalibrateRotation(bool value) {
        isCalibratingPosition = false;
        isCalibratingRotation = value;
        if (value) {
            OnCalibrationStart.Invoke();
        } else {
            OnCalibrationEnd.Invoke();
        }
    }

    public void EnableCalibratePosition(bool value) {
        isCalibratingRotation = false;
        isCalibratingPosition = value;
        if (value) {
            OnCalibrationStart.Invoke();
        } else {
            OnCalibrationEnd.Invoke();
        }
    }

    private void CalibrateRotation() {
        if (isCalibratingRotation) {
            // DISABLE OTHER CONTROLS AND ENABLE TOUCH CONTROL UI

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)

            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    initialTouchPosition = touch.position;
                    initialObjectPosition = transform.position;
                } else if (touch.phase == TouchPhase.Moved) {
                    Vector3 currentTouchPosition = new Vector3(touch.position.x, touch.position.y, 10);
                    Vector3 deltaTouchPosition = Camera.main.ScreenToWorldPoint(currentTouchPosition) - initialTouchPosition;

                    // Allow movement only in the x and z axes
                    transform.position = new Vector3(initialObjectPosition.x + deltaTouchPosition.x * 0.01f, initialObjectPosition.y, initialObjectPosition.z + deltaTouchPosition.y * 0.01f);
                }
            }
#else
            if (Input.GetMouseButtonDown(0)) {
                initialTouchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                initialObjectRotation = transform.localEulerAngles;
            } else if (Input.GetMouseButton(0)) {
                Vector3 currentTouchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                Vector3 deltaTouchPosition = Camera.main.ScreenToWorldPoint(currentTouchPosition) - Camera.main.ScreenToWorldPoint(initialTouchPosition);

                // Allow movement only in the x and z axes
                transform.localEulerAngles = initialObjectRotation + new Vector3(deltaTouchPosition.x, 0, deltaTouchPosition.y);
            }
#endif  

        }
    }
    private void CalibratePosition() {
        if (isCalibratingPosition) {
            // DISABLE OTHER CONTROLS AND ENABLE TOUCH CONTROL UI

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)

            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    initialTouchPosition = touch.position;
                    initialObjectPosition = transform.position;
                } else if (touch.phase == TouchPhase.Moved) {
                    Vector3 currentTouchPosition = new Vector3(touch.position.x, touch.position.y, 10);
                    Vector3 deltaTouchPosition = Camera.main.ScreenToWorldPoint(currentTouchPosition) - initialTouchPosition;

                    // Allow movement only in the x and z axes
                    transform.position = initialObjectPosition + new Vector3(deltaTouchPosition.x*0.01f, 0, deltaTouchPosition.y * 0.01f);
                }
            }
#else
            if (Input.GetMouseButtonDown(0)) {
                initialTouchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                initialObjectPosition = transform.position;
            } else if (Input.GetMouseButton(0)) {
                Vector3 currentTouchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                Vector3 deltaTouchPosition = Camera.main.ScreenToWorldPoint(currentTouchPosition) - Camera.main.ScreenToWorldPoint(initialTouchPosition);

                // Allow movement only in the x and z axes
                transform.position = initialObjectPosition + new Vector3(deltaTouchPosition.x, 0, deltaTouchPosition.y);
            }
#endif  

        }
    }
}
