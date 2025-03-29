using UnityEngine;

[RequireComponent(typeof(InputHandler))]

public class Player : MonoBehaviour {

    InputHandler _inputHandler;

    FirstPersonMovement _firstPerson;

    [SerializeField] float _moveSpeed = 5.0f;
    [SerializeField] float _rotationSpeed;

    [Header("Camera Settings")]
    [SerializeField] Transform _cinemachineTarget;
    [SerializeField] float _bottomClamp;
    [SerializeField] float _topClamp;
    [SerializeField] float _cameraSensitivity;

    void Start() {
        _inputHandler = GetComponent<InputHandler>();

        _firstPerson = new(transform, _inputHandler);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        _firstPerson.Move(_moveSpeed);
    }

    void LateUpdate() {
        _firstPerson.CameraFollow(_cinemachineTarget, _bottomClamp, _topClamp, _cameraSensitivity); 
    }
}

