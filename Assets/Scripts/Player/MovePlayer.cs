using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200;

    private Rigidbody2D _rb;

    private Vector2 _moveVec;

    private float _inputX;
    private float _inputY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _moveVec = new Vector2(
            _inputX,
            _inputY
        );

        _moveVec = _moveVec.normalized * moveSpeed * Time.fixedDeltaTime;

        _rb.velocity = _moveVec;
    }
}
