using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 rawInput;

    protected void Update()
    {
        Move();
    }

    protected void Move()
    {
        Vector3 delta = speed * Time.deltaTime * rawInput;
        transform.position += delta;
    }

    protected void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log("Move: " + rawInput);
    }
}
