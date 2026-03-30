using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SprayColor : MonoBehaviour
{
    public PointerMovement pointerScript; //For the position of the pointer
    public ParticleSystem particles;
    public TextMeshProUGUI timerText;

    public UnityEvent colorSprayed;

    float cooldownTimer = 0, cooldown = 1.5f; //In seconds

    bool hasGameEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            timerText.text = cooldownTimer.ToString("F2"); //Showing digits after the dot because the cooldown is too short to only use seconds
        }
        else
            timerText.text = "";
    }

    public void sprayColor()
    {
        //If the timer has reached the amount set and if the pointer is above the main painter (if its x value is the same)
        if (cooldownTimer <= 0 && transform.position.x == pointerScript.pointerPositions[pointerScript.currentPos])
        {
            //I am again assuming that we are allowed to use functions relating to the particle system, I can see that
            //startColor is deprecated but I couldn't figure out a better way other than needing three particle systems for each color
            particles.startColor = gameObject.GetComponent<SpriteRenderer>().color;
            particles.Play();
            cooldownTimer = cooldown;
            colorSprayed.Invoke();
        }
    }
}
