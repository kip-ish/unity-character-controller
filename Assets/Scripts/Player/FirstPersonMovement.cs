using UnityEngine;

public class FirstPersonMovement {
    
    Transform _transform;
    InputHandler _input;
    Camera _camera;

    Vector3 _movement;

    float _yaw;
    float _pitch;

    public FirstPersonMovement(Transform transform, InputHandler input) {
        _transform = transform;
        _input = input;
        _camera = Camera.main;
    }

    public void Move(float moveSpeed) {
        // get move input from InputHandler class
        _movement = new(_input.MoveNormalized.x, 0.0f, _input.MoveNormalized.y);

        // Move depends on the camera direction
        Vector3 moveDir = _camera.transform.forward * _movement.z 
        + _camera.transform.right * _movement.x;

        // Set Y axis to 0 for player to stay in the ground
        moveDir.y = 0f;
        
        // move the player
        _transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    public void CameraFollow(Transform cameraFollowTransform, float bottomClamp,
    float topClamp, float cameraSensitivity) {

        float threshold = 0.01f;
        if(_input.Look.magnitude > threshold) {
            _yaw = _input.Look.x * Time.deltaTime * cameraSensitivity;
            _pitch += -_input.Look.y * Time.deltaTime * cameraSensitivity;

            _yaw = ClampAngle(_yaw, float.MinValue, float.MaxValue);
            _pitch = ClampAngle(_pitch, bottomClamp, topClamp);

            cameraFollowTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);
            _transform.Rotate(Vector3.up * _yaw);
        }
    }

    float ClampAngle(float target, float min, float max) {
        if(target < -360) target += 360;
        if(target > 360) target -= 360;
        return Mathf.Clamp(target, min, max);
    }
}
