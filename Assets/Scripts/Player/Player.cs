using UnityEngine;

[RequireComponent(typeof(InputHandler))]

public class Player : MonoBehaviour {

    InputHandler _inputHandler;

    ThirdPersonMovement _thirdPerson;

    [SerializeField] float _moveSpeed = 5.0f;
    [SerializeField] float _rotationSpeed;

    [Header("Camera Settings")]
    [SerializeField] Transform _cinemachineTarget;
    [SerializeField] float _bottomClamp;
    [SerializeField] float _topClamp;
    [SerializeField] float _cameraSensitivity;

    void Start() {
        _inputHandler = GetComponent<InputHandler>();

        _thirdPerson = new(transform, _inputHandler);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        _thirdPerson.Move(_moveSpeed, _rotationSpeed);
    }

    void LateUpdate() {
        _thirdPerson.CameraFollow(_cinemachineTarget, _bottomClamp, _topClamp, _cameraSensitivity); 
    }
}

