using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PointerMovement : MonoBehaviour
{
    public List<Transform> pointedObjects;
    public UnityEvent onActionCalled;

    //pointerPositions holds the x coordinates that the player moves between when moving left and right
    public float[] pointerPositions = new float[3];
    public int currentPos = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets pointerPositions to have the same amount of x points as the objects it needs to point to
        pointerPositions = new float[pointedObjects.Count];
        //Set the poisition the pointer is going to point to based on the center position of the objects
        for (int i = 0; i < pointerPositions.Length; i++)
        {
            pointerPositions[i] = pointedObjects[i].position.x;
        }
    }

    public void movePointer(InputAction.CallbackContext context)
    {
        //Only move if there is another place to move to and only on started (onClick)
        if (pointerPositions.Length > 0 && context.started)
        {
            //Move right (input > 0)
            if (context.ReadValue<Vector2>().x > 0)
            {
                //Cycle down in the array
                if (currentPos == 0)
                    currentPos = pointerPositions.Length - 1;
                else
                    currentPos--;
            }
            //Move left (input < 0)
            else if (context.ReadValue<Vector2>().x < 0)
            {
                //Cycle up in the array
                if (currentPos == pointerPositions.Length - 1)
                    currentPos = 0;
                else
                    currentPos++;
            }
            transform.position = new Vector3(pointerPositions[currentPos], transform.position.y, transform.position.z);
        }
    }

    public void callAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //When the action button is pressed invoke the event
            onActionCalled.Invoke();
        }
    }
}
