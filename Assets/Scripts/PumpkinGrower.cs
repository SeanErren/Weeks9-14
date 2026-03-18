using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinGrower : MonoBehaviour
{
    public Transform tree;
    public List<Transform> pumpkins;

    Coroutine startCoroutines, growTree, growPumpkin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetElements();
    }

    public void StartGrowing()
    {
        //Making sure to remove older Coroutines before starting new ones
        if (startCoroutines != null)
        {
            StopCoroutine(startCoroutines);
        }
        if (growTree != null)
        {
            StopCoroutine(growTree);
        }
        if (growPumpkin != null)
        {
            StopCoroutine(growPumpkin);
        }
        StartCoroutine(GrowElements());
    }

    IEnumerator GrowElements()
    {
        //There is a stopAllCorutines function which stops all the Coroutines for the specific script
        //Assigning the variable while initializing the Coroutine
        yield return growTree = StartCoroutine(GrowTree());
        yield return growPumpkin = StartCoroutine(GrowPumpkins());
    }

    IEnumerator GrowTree()
    {
        ResetElements();
        float time = 0;

        while (time < 1)
        {
            tree.localScale = Vector2.one * time;
            time += Time.deltaTime;

            yield return null;
        }
        
    }

    IEnumerator GrowPumpkins()
    {
        float time = 0;
        resetPumpkins();

        for (int i = 0; i < pumpkins.Count; i++)
        {
            time = 0;
            while (time < 1)
            {
                pumpkins[i].localScale = Vector2.one * time;
                time += Time.deltaTime;

                yield return null;
            }
        }
    }

    void ResetElements()
    {
        tree.localScale = Vector2.zero;
        resetPumpkins();
    }
    void resetPumpkins()
    {
        for (int i = 0; i < pumpkins.Count; i++)
        {
            pumpkins[i].localScale = Vector2.zero;
        }
    }
}
