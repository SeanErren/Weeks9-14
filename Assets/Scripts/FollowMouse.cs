using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    public Vector2 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    public void onPoint(InputAction.CallbackContext context)
    {
        //Getting the vector 2 from the context returns the mouse position
        //While convoluted, it attaches the event / the mouse movement to a specific object's movement componant which allows for saparation of inputs
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
