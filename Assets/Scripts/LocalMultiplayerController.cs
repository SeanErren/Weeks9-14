using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerController : MonoBehaviour
{
    Vector2 movementInput = new();
    public PlayerInput playerInput;

    public float speed = 3;

    public LocalMultiplayerManager manager;

    //Coroutine
    Coroutine squeeze;
    public float squeezeMin = 0.5f;
    public AnimationCurve animationCurve;
    public float curveTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movementInput * speed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Player " + playerInput.playerIndex + ": Attack!!!");
            //Stop the existing coroutine
            if (squeeze != null)
            {
                StopCoroutine(squeeze);
                squeeze = null;
            }
            //Start a new coroutine
            squeeze = StartCoroutine(SqueezePlayer());

            manager.PlayerAttacking(playerInput);
        }
    }

    IEnumerator SqueezePlayer()
    {
        curveTime = 0;
        while (transform.localScale.x > squeezeMin && transform.localScale.y > squeezeMin)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime);
            curveTime += Time.deltaTime;
            yield return null;
        }
        curveTime = 0;
        while (transform.localScale.x < 1 && transform.localScale.y < 1)
        {
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime);
            curveTime += Time.deltaTime;
            yield return null;
        }
        curveTime = 0;
    }
}
