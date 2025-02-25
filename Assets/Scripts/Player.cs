using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 rawInput;

    protected void Update()
    {
        
    }

    protected void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log("Move: " + rawInput);
    }
}
