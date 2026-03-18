using System.Collections;
using UnityEngine;

public class PumpkinGrower : MonoBehaviour
{
    public Transform tree;
    public Transform pumpkin;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetElements();
    }

    public void StartGrowing()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        ResetElements();
        float time = 0;

        while (time < 1)
        {
            tree.localScale = Vector2.one * time;
            time += Time.deltaTime;

            yield return null;
        }

        time = 0;

        while (time < 1)
        {
            pumpkin.localScale = Vector2.one * time;
            time += Time.deltaTime;

            yield return null;
        }

    }

    void ResetElements()
    {
        tree.localScale = Vector2.zero;
        pumpkin.localScale = Vector2.zero;
    }
}
