using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MugSpawner : MonoBehaviour
{
    //This script serves as a main script of sorts because it handles the mugs and the mugs are the element dictating the score
    //The prefab
    public GameObject mug;
    public SpriteRenderer painterHitbox;
    public SpriteRenderer crusherHitbox;

    List<GameObject> mugs = new();
    float startingX = 10, distanceBetweenMugs = 6; //In meters (the distance is from the center of the mugs)
    public GameState gameState = GameState.PRESTART;
    public GameObject startGameCanvasElements, endGameCanvasElements;
    public UnityEvent endGame, startGame;

    //The main painter for its color
    public SpriteRenderer painter;
    public ParticleSystem particles;

    //Desired color
    public SpriteRenderer painterLeft, painterRight;
    public SpriteRenderer visibleDesiredColor;
    List<Color> desiredColors = new();
    public TextMeshProUGUI scoreText;
    int score = 0;
    
    public static bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.PRESTART:
                break;
            case GameState.PLAY:
                {
                    if (!isPaused)
                    {
                        //Instantiate a new mug if the last spawned mug passes a distance threshold (or if there are no mugs)
                        if (mugs.Count == 0 || mugs[mugs.Count - 1].transform.position.x + distanceBetweenMugs < startingX)
                        {
                            mugs.Add(Instantiate(mug, new Vector3(startingX, -0.25f, 0), Quaternion.identity));
                            int tempRandom = Random.Range(0, 3); //Get a random integer (0 - 2)
                            switch(tempRandom) //Based on the number give the desired color a corrosponding painter
                            {
                                case 0:
                                    desiredColors.Add(painter.color);
                                    visibleDesiredColor.color = painter.color;
                                    break;
                                case 1:
                                    desiredColors.Add(painterLeft.color);
                                    visibleDesiredColor.color = painterLeft.color;
                                    break;
                                case 2:
                                    desiredColors.Add(painterRight.color);
                                    visibleDesiredColor.color = painterRight.color;
                                    break;
                            }
                        }
                        //If the mug passed a meter beyond the edge of the screen destroy it
                        if (mugs.Count != 0 && mugs[0].transform.position.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1)
                        {
                            //If the first mug's color is equal to its color requirement
                            //Always checking the first because the first is the furthest and the one to trigger the above if statement
                            if (mugs[0].GetComponent<SpriteRenderer>().color == desiredColors[0])
                            {
                                desiredColors.RemoveAt(0); //Remove the checked color
                                //Up the score
                                if (mugs[0].transform.localScale.y == 1)
                                {
                                    score++;
                                    scoreText.text = "Score: " + score;
                                }
                                //If the local scale is not 1 it means that the object has been crushed and doesn't count for any points
                            }
                            //If the object has not been crushed and doesn't have the right color end the game
                            else if (mugs[0].transform.localScale.y == 1)
                            {
                                //Pause all movements, wrong color, the game is over
                                InfiniteConveyor.isPaused = true; //Belts
                                ConstantMovement.isPaused = true; //Wheels
                                isPaused = true; //Mugs
                                endGame.Invoke(); //Tell the other classes that the game has ended
                                endGameCanvasElements.SetActive(true);
                                gameState = GameState.END;
                            }
                            else
                            {
                                desiredColors.RemoveAt(0); //Remove the checked color
                            }

                            Destroy(mugs[0]);
                            mugs.RemoveAt(0);
                        }
                    }   
                }
                break;
            case GameState.END:
                break;
        }
    }
    //Called on Unity event
    public void colorMug()
    {
        StartCoroutine(colorMugChecks());
    }
    //Runs until the particles stop spraying to keep checking if the mugs are within the bounds
    IEnumerator colorMugChecks()
    {
        //Checks if the mugs are withing the bounds and lives until the particles stop emitting
        while (particles.isEmitting)
        {
            //Go throught the mugs
            for (int i = 0; i < mugs.Count; i++)
            {
                //If the hitbox for the painter contains a mug color it
                if (painterHitbox.bounds.Contains(mugs[i].transform.position))
                {
                    mugs[i].GetComponent<SpriteRenderer>().color = painter.color;
                }
            }
            yield return null;
        }
    }
    //Called on Unity event
    public void crushMug()
    {
        //Go through the mugs
        for (int i = 0;i < mugs.Count;i++)
        {
            //If the mug is within the hitbox of the press crush it
            if (crusherHitbox.bounds.Contains(mugs[i].transform.position))
            {
                mugs[i].transform.localScale -= new Vector3(0, 0.5f, 0);
                mugs[i].transform.position -= new Vector3(0, 0.1f, 0);
            }
        }
    }
    //Also used to start off the game at the beginning
    public void Restart()
    {
        //Remove start screen elements
        startGameCanvasElements.SetActive(false);
        //Remove end screen elements
        endGameCanvasElements.SetActive(false);
        //Reset score
        score = 0;
        scoreText.text = "Score: 0";
        //Destroy all the mugs
        foreach(GameObject mug in mugs)
        {
            Destroy(mug);
        }
        //Clear the lists
        mugs = new();
        desiredColors = new();
        //Tell other functions that the game has restarted
        startGame.Invoke();
        //Set the movement objects to move again
        InfiniteConveyor.isPaused = false; //Belts
        ConstantMovement.isPaused = false; //Wheels
        isPaused = false; //Mugs
        //Set the state to play
        gameState = GameState.PLAY;
    }
}

public enum GameState
{
    PRESTART, PLAY, END
}
