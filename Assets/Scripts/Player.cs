using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 rawInput;

    private Vector2 minBound;
    private Vector2 maxBound;

    private float paddingTop;
    private float paddingRight;

    #region unity methods
    protected void Start()
    {
        InitBounds();
    }

    protected void Update()
    {
        Move();
    }

    protected void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    protected void OnAttack(InputValue value)
    {

    }
    #endregion

    #region
    private void InitBounds()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        paddingTop = spriteRenderer.bounds.size.y / 2;
        paddingRight = spriteRenderer.bounds.size.x / 2;

        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(Vector2.zero);
        maxBound = mainCamera.ViewportToWorldPoint(Vector2.one);

        minBound.x += paddingRight;
        minBound.y += paddingTop;

        maxBound.x -= paddingRight;
        maxBound.y -= paddingTop;
    }

    private void Move()
    {
        Vector2 delta = speed * Time.deltaTime * rawInput;

        Vector2 newPos = new()
        {
            x = Mathf.Clamp(transform.position.x + delta.x, minBound.x, maxBound.x),
            y = Mathf.Clamp(transform.position.y + delta.y, minBound.y, maxBound.y)
        };

        transform.position = newPos;
    }
    #endregion
}
