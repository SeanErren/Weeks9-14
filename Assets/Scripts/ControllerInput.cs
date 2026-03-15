using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void onAttack (InputAction.CallbackContext context)
    {
        //context has 3 values: .started, .performed, .cancled
        //The values are basically onClick, wasPressedThisFrame, onRelease
        if (context.performed) //.performed only takes place ones even though it takes place after click
        {
            Debug.Log("Attack!!!");
        }
    }
}
