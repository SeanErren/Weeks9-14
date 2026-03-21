using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public LineRenderer lr;
    //remember all the lines in the list for deletion
    List<Vector2> points = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        //Setting the starting position of the line to 2, 2 meters
        points.Add(new Vector2 (2, 2));

        updateLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Only add a new coordinate to the line when the mouse is pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Adding a new point to the list based on the position of the mouse
            points.Add(mousePos);
            updateLineRenderer(); //Redrawing the lines
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //If there is a point in the list to remove
            if (points.Count > 0)
            {
                //Delete the first element (instead of last), used for removing a completed movement for example
                points.RemoveAt(0);
                updateLineRenderer();
            }
        }
    }

    //Refreshes all the lines in the line renderer based on the list
    void updateLineRenderer()
    {
        lr.positionCount = points.Count; //Setting lr to have the same amount of variables as the list
        for (int i = 0; i < points.Count; i++)
        {
            //Adding a new point to the line based on the list
            lr.SetPosition(i, points[i]);
        }
    }
}
