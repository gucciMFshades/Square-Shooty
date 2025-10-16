using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    
    public float speed { get; set; } = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Vector2 mousePos;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    private void Update() => mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f
        );

        _rigidbody.linearVelocity = _smoothedMovementInput * speed;

        Vector2 lookDir = mousePos - _rigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle;
    }

    private void OnMove(InputValue inputValue) => _movementInput = inputValue.Get<Vector2>();
}



