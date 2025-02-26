using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 rawInput;

    private Vector2 minBound;
    private Vector2 maxBound;

    protected void Start()
    {
        InitBounds();
    }

    protected void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(Vector2.zero);
        maxBound = mainCamera.ViewportToWorldPoint(Vector2.one);
    }

    protected void Move()
    {
        Vector2 delta = speed * Time.deltaTime * rawInput;

        Vector2 newPos = new()
        {
            x = Mathf.Clamp(transform.position.x + delta.x, minBound.x, maxBound.x),
            y = Mathf.Clamp(transform.position.y + delta.y, minBound.y, maxBound.y)
        };

        transform.position = newPos;
    }

    protected void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log("Move: " + rawInput);
    }
}
