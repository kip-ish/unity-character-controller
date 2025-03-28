using UnityEngine;


public class InputHandler : MonoBehaviour {
    
    InputActions _input;

    void Awake() {
        _input = new();
        _input.Player.Enable();
    }

    public Vector2 MoveNormalized => _input.Player.Move.ReadValue<Vector2>().normalized;
    public Vector2 Look => _input.Player.Look.ReadValue<Vector2>();
}
