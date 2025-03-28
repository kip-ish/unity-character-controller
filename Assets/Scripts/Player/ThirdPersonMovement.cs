using UnityEngine;

public class ThirdPersonMovement {

    Transform _transform;
    InputHandler _input;
    Camera _camera;

    Vector3 _movement;

    float _pitch;
    float _yaw;


    public ThirdPersonMovement(Transform transform, InputHandler input) {
        _transform = transform;
        _input = input;
        _camera = Camera.main;
    }

    public void Move(float moveSpeed, float rotationSpeed) {
        // get move input from InputHandler class
        _movement = new(_input.MoveNormalized.x, 0.0f, _input.MoveNormalized.y);

        // Move depends on the camera direction
        Vector3 moveDir = _camera.transform.forward * _movement.z 
        + _camera.transform.right * _movement.x;

        // Set Y axis to 0 for player to stay in the ground
        moveDir.y = 0f;
        
        // move the player
        _transform.position += moveSpeed * Time.deltaTime * moveDir;

        _transform.forward = Vector3.Slerp(_transform.forward, moveDir, 
        rotationSpeed * Time.deltaTime);
    }

    public void CameraFollow(Transform cameraFollowTransform, 
    float bottomClamp, float topClamp, float cameraSensitivity) {

        float threshold = 0.01f;
        if(_input.Look.magnitude > threshold) {
            _yaw += _input.Look.x * Time.deltaTime * cameraSensitivity;
            _pitch += -_input.Look.y * Time.deltaTime * cameraSensitivity;
        }

        _yaw = ClampAngle(_yaw, float.MinValue, float.MaxValue);
        _pitch = ClampAngle(_pitch, bottomClamp, topClamp);
        
        cameraFollowTransform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
    }

    float ClampAngle(float target, float min, float max) {
        if(target < -360) target += 360;
        if(target > 360) target -= 360;
        return Mathf.Clamp(target, min, max);
    }
}
