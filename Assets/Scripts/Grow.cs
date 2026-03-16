using System.Collections;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public GameObject tree;
    public GameObject apple;

    //The amount of seconds until an apple is grown
    float waitUntilApple = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree.transform.localScale = Vector3.zero;
        apple.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator starts a coderutine which means that it's a function that never dies.
    //The function will stop whenever it reaches the yield type and continue from that point next frame.
    //In a sense, the function behaves like a temporary Update but it only starts when it's initiated
    IEnumerator GrowTree() //The function requires StartCoroutine which can not be initiated using a button click
    {
        float time = 0;
        //Reset to 0 incase the button gets recalled
        tree.transform.localScale = Vector3.zero;
        apple.transform.localScale = Vector3.zero;

        while (time < 1)
        {
            time += Time.deltaTime;
            tree.transform.localScale = Vector3.one * time;
            //yield for pausing, return for continuing to the next thing and null because we don't use the returned value
            yield return null;
            //Since yield is inside of the loop it is not going to leave until the loop is done
        }

        time = 0;

        //Returning a new instance of a class that tells unity how long to halt for
        //MORE EFFICIENT but similar to yielding within an empty loop that counts until an amount is reached
        yield return new WaitForSeconds(waitUntilApple);

        while (time < 1)
        {
            time += Time.deltaTime;
            apple.transform.localScale = Vector3.one * time;
            yield return null;
        }
    }
    //Initializes GrowTree as a forever function
    public void StartGrowTreeRoutine()
    {
        StartCoroutine(GrowTree());
    }
}
