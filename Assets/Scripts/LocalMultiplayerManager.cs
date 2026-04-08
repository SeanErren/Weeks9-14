using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerManager : MonoBehaviour
{
    public List<Sprite> playerSprites;
    List<PlayerInput> players = new();

    public float range = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJoin(PlayerInput player)
    {
        players.Add(player);
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[player.playerIndex];

        player.GetComponent<LocalMultiplayerController>().manager = this;
    }

    public void PlayerAttacking(PlayerInput attackingPlayer)
    {
        for (int i = 0; i < players.Count; i++)
        {
            //Making sure that the player is not attacking themselves
            if (attackingPlayer == players[i]) continue;

            //If the distance between the player hitting and another player is within the other player's range - attack them
            if (Vector2.Distance(attackingPlayer.transform.position, players[i].transform.position) < range)
            {
                Debug.Log("Player " + attackingPlayer.playerIndex + " hit player " + players[i].playerIndex);
            }
        } 
    }
}
