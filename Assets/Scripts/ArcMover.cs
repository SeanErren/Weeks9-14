using System.Collections;
using UnityEngine;

public class ArcMover : MonoBehaviour
{
    public Transform movedObject;

    Coroutine startRoutines, rightRoutine, downRoutine, leftRoutine;

    float degreeLimitRight = 5, degreeLimitDown, degreeLimitLeft;
    float scaler = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startAnimation();
    }

    public void startAnimation()
    {
        if (startRoutines == null)
        {
            if (rightRoutine != null)
            {
                StopCoroutine(rightRoutine);
            }
            if (downRoutine != null)
            {
                StopCoroutine(downRoutine);
            }
            if (leftRoutine != null)
            {
                StopCoroutine(leftRoutine);
            }
            startRoutines = StartCoroutine(StartRoutines());
        }
    }

    IEnumerator StartRoutines()
    {
        yield return rightRoutine = StartCoroutine(diagonalRight());
        yield return downRoutine = StartCoroutine(diagonalDown());
        yield return leftRoutine = StartCoroutine(diagonalLeft());
    }

    IEnumerator diagonalRight()
    {
        float degree = 0, degreeLimit = 5;

        while (degree < degreeLimit)
        {
            movedObject.position = new Vector3(Mathf.Sin(degree * 2) * scaler, Mathf.Cos(-degree) * scaler, 0);
            Debug.Log(degree);
            
            yield return null;
            degree += Time.deltaTime;
        }
    }
    IEnumerator diagonalDown()
    {
        yield return null;
    }
    IEnumerator diagonalLeft()
    {
        yield return null;
    }
}
