using UnityEngine;

public class SprayColor : MonoBehaviour
{
    public PointerMovement pointerScript; //For the position of the pointer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testEvent()
    {
        //If the pointer is above the main painter (if its x value is the same)
        if (transform.position.x == pointerScript.pointerPositions[pointerScript.currentPos])
        {
            gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
}
