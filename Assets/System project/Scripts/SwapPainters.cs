using UnityEngine;
using UnityEngine.InputSystem;

public class SwapPainters : MonoBehaviour
{
    public SpriteRenderer mainPainter, leftPainter, rightPainter;

    public ParticleSystem ParticleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cycles the painters' colors on interact input (E)
    public void SwapColors(InputAction.CallbackContext context)
    {
        //Only allow swapping painters if the particle system is not emitting
        //I assume I am allowed to use IsAlive() because it's a particle system function and we didn't go through everything
        //in the particle system because it's just too big
        if (!ParticleSystem.isEmitting)
        {
            Color saveColor = rightPainter.color;
            if (context.started)
            {
                rightPainter.color = mainPainter.color;
                mainPainter.color = leftPainter.color;
                leftPainter.color = saveColor;
            }
        }
    }
}
