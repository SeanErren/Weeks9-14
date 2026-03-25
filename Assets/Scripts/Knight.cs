using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public AudioSource footsetpSound;
    public List<AudioClip> audioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FootStep()
    {
        int randomClip = Random.Range(0, audioClips.Count - 1);
        footsetpSound.clip = audioClips[randomClip];

        //SAVE PROJECT INSTEAD OF SAVE SCENE WILL MAKE SURE THAT THE ANIMATION LAYOUT WILL BE SAVED (should be saved anyway but just in case
        footsetpSound.Play();
    }
}
