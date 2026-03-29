using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSpawner : MonoBehaviour
{
    //This script serves as a main script of sorts because it handles the mugs and the mugs are the element dictating the score
    //The prefab
    public GameObject mug;
    public SpriteRenderer painterHitbox;
    public SpriteRenderer crusherHitbox;

    List<GameObject> mugs = new();
    float startingX = 10, distanceBetweenMugs = 8; //In meters (the distance is from the center of the mugs)
    GameState gameState = GameState.PRESTART;

    //The main painter for its color
    public SpriteRenderer painter;
    public ParticleSystem particles;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.PLAY;
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
                    //Instantiate a new mug if the last spawned mug passes a distance threshold (or if there are no mugs)
                    if (mugs.Count == 0 || mugs[mugs.Count - 1].transform.position.x + distanceBetweenMugs < startingX)
                        mugs.Add(Instantiate(mug, new Vector3(startingX, 0, 0), Quaternion.identity));
                    //If the mug passed a meter beyond the edge of the screen destroy it
                    if (mugs.Count != 0 && mugs[0].transform.position.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1)
                    {
                        Destroy(mugs[0]);
                        mugs.RemoveAt(0);
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
}

public enum GameState
{
    PRESTART, PLAY, END
}
