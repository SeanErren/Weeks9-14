using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Crush : MonoBehaviour
{
    //For the height from the prefab
    public Transform pressTop;
    public Transform conveyorTop;

    public PointerMovement pointerScript;

    float downSpeed = 8, upSpeed = 5; //meters per second
    bool isGoingDown = false; 

    Coroutine pressDown, pressUp;

    public UnityEvent onCrush;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void movePress()
    {
        //If the x of the pointer is over the x over the presser
        if (transform.position.x == pointerScript.pointerPositions[pointerScript.currentPos])
        {
            //If not going down go down, if yes go up assuming that that action is not already being done
            if (!isGoingDown && pressDown == null)
            {
                isGoingDown = true;
                //If the press is going up, stop it
                if (pressUp != null)
                {
                    StopCoroutine(pressUp);
                    pressUp = null;
                }
                startPressDown();
            }
            else if (isGoingDown && pressUp == null)
            {
                isGoingDown = false;
                //If the press is going down, stop it
                if (pressDown != null)
                {
                    StopCoroutine(pressDown);
                    pressDown = null;
                }
                startGoUp();
            }
        }
    }
    void startPressDown()
    {
        isGoingDown = true;
        pressDown = StartCoroutine(PressDown());
    }
    void startGoUp()
    {
        isGoingDown = false;
        pressUp = StartCoroutine(GoUp());
    }

    //Coroutine to push the piston down
    IEnumerator PressDown()
    {
        //While the piston is above the conveyor belt push it down
        while (transform.position.y > conveyorTop.position.y)
        {
            transform.position -= new Vector3(0, downSpeed * Time.deltaTime, 0);
            yield return null;
        }
        pressDown = null;
        isGoingDown = false;
        onCrush.Invoke();
        pressUp = StartCoroutine(GoUp());
    }
    //Coroutine to push the piston back up
    IEnumerator GoUp()
    {
        isGoingDown = false;
        //While the piston is below its starting point push it up
        while (transform.position.y < pressTop.position.y)
        {
            transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
            yield return null;
        }
        pressUp = null;
    }
}
