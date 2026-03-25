using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTriangle : MonoBehaviour
{
    public float speed = 3; //meters / second
    Vector2 movement = new Vector2();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is not hitting the left or right buttons the x of movement will be 0 and no movement will take place
        transform.position += new Vector3(movement.x * speed * Time.deltaTime, 0, 0);
        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width)
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Vector3.zero).x, transform.position.y, 0);
        else if (Camera.main.WorldToScreenPoint(transform.position).x < 0)
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x, transform.position.y, 0);
    }

    public void onMove(InputAction.CallbackContext context)
    {
        //Change the movement variable to whatever the vector of movement the player inputted (W A S D)
        movement = context.ReadValue<Vector2>();
    }
}
