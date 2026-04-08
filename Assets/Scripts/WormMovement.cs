using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WormMovement : MonoBehaviour
{
    Vector2 mousePos = Vector2.zero; //In world space (meters)
    Coroutine movement;
    float speed = 5; //Meters per second

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movement);
    }
    public void updateMousePos(InputAction.CallbackContext context)
    {
        if (context.performed)
            mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void MoveWorm(InputAction.CallbackContext context)
    {
        if (movement == null)
        {
            if (context.performed)
            {
                movement = StartCoroutine(Movement());
            }
        }  
    }

    IEnumerator Movement()
    {
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 endPos = mousePos;
        while (time < 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time);
            time += Time.deltaTime / Vector2.Distance(startPos, endPos) * speed;
            yield return null;
        }
        movement = null;
    }
}
