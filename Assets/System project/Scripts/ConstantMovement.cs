using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public int direction = 0; //0 = left, 1 = right, 2 = spin
    public int speed = 3; //Meters per second
    public int angleSpeed = 200; //Angle per second

    //I can't remember if we are allowed to use static but I figured that since I will need all elements moving to stop at the same time I
    //figured that having a variable outside of the specific class instance that all instanses use would be easier than stopping them one by one.
    public static bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            switch (direction)
            {
                case 0:
                    transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
                    break;
                case 1:
                    transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                    break;
                case 2:
                    transform.eulerAngles += new Vector3(0, 0, angleSpeed) * Time.deltaTime;
                    break;
            }
        }
    }
}
