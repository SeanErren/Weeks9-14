using UnityEngine;

public class InfiniteConveyor : MonoBehaviour
{
    public GameObject conveyor; //The prefab
    bool isMovingRight = false; //Movement direction

    GameObject copy1 = null;
    GameObject copy2 = null;

    public static bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //If the conveyor prefab is moving right set spawning to left, else - set spawining to right
        if (conveyor.GetComponent<ConstantMovement>().direction == 1)
            isMovingRight = true;
        else
            isMovingRight = false;


            copy1 = Instantiate(conveyor);
        if (isMovingRight)
            //Using the scale to know how far away from the first to spawn
            copy2 = Instantiate(conveyor, new Vector3(-conveyor.transform.localScale.x, conveyor.transform.position.y, conveyor.transform.position.z), Quaternion.identity);
        else
            copy2 = Instantiate(conveyor, new Vector3(conveyor.transform.localScale.x, conveyor.transform.position.y, conveyor.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (isMovingRight)
            {
                //If the out of screen conveyor reaches the center
                if (copy2.transform.position.x > 0)
                {
                    Destroy(copy1);
                    copy1 = copy2;
                    copy2 = Instantiate(conveyor, new Vector3(-conveyor.transform.localScale.x, conveyor.transform.position.y, conveyor.transform.position.z), Quaternion.identity);
                }
            }
            else
            {
                if (copy2.transform.position.x < 0)
                {
                    Destroy(copy1);
                    copy1 = copy2;
                    copy2 = Instantiate(conveyor, new Vector3(conveyor.transform.localScale.x, conveyor.transform.position.y, conveyor.transform.position.z), Quaternion.identity);
                }
            }
        }
    }
}
