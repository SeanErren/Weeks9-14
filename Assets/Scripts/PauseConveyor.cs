using System.Collections;
using TMPro;
using UnityEngine;

public class PauseConveyor : MonoBehaviour
{
    public PointerMovement pointerScript;
    public TextMeshProUGUI timerText;

    float waitTime = 3; //Seconds

    bool hasStopped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopConveyor()
    {
        //If it hasn't stopped already and if the pointer is above the stop button
        if (!hasStopped && transform.position.x == pointerScript.pointerPositions[pointerScript.currentPos])
        {
            InfiniteConveyor.isPaused = true; //Belts
            ConstantMovement.isPaused = true; //Wheels
            MugSpawner.isPaused = true; //Mugs

            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        float timer = waitTime;
        //yield return new WaitForSeconds(waitTime); //Not useing to show the timer on the textMeshPro
        while(timer > 0)
        {
            timerText.text = ((int)timer).ToString();
            yield return 
            timer -= Time.deltaTime;
        }

        timerText.text = "";

        hasStopped = true; //Pause can only be called once

        InfiniteConveyor.isPaused = false; //Belts
        ConstantMovement.isPaused = false; //Wheels
        MugSpawner.isPaused = false; //Mugs
    }
}
