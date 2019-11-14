using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    //Objects and Components:
    private AudioSource audioSource; //Temporarily get audio source to play music
    private List<GameObject> sheebs = new List<GameObject>(); //A list of all sheebs currently in scene
    private List<Animator> sheebAnimators = new List<Animator>(); //List of animators of all sheebs in scene
        GameObject[] redSheebsInScene; //Initialize sheeb tracker
        GameObject[] blueSheebsInScene; //Initialize sheeb tracker
        GameObject[] greenSheebsInScene; //Initialize sheeb tracker

[Header("Settings:")]
    public float startDelay; //How long to wait after playing song to begin first beat
    public float beatsPerMinute; //Tempo of given song
    public float bouncesPerBeat; //How many times each sheep will bounce per beat
    public float bounceSpeed; //How fast each part of each sheep bounce is

    //Tracker Variables:
    private float musicStartTime; //What time (in real time) music started
    private float timeSinceBeat = 0; //How much time has passed since last beat

    void Start()
    {
        //Get Objects and Components:
        audioSource = GetComponent<AudioSource>(); //Get audioSource
        GetSheebs(); //Get any starting sheebs in scene
    }

    void FixedUpdate()
    {
        //Update Sheeb Lists:
        if (
            redSheebsInScene != GameObject.FindGameObjectsWithTag("Red_Sheeb") ||
            blueSheebsInScene != GameObject.FindGameObjectsWithTag("Blue_Sheeb") ||
            greenSheebsInScene != GameObject.FindGameObjectsWithTag("Green_Sheeb")
           ) { GetSheebs(); } //If any new sheep have been added to scene since last check, update sheeb lists

        //Start Music:
        if (audioSource.isPlaying == false && Time.realtimeSinceStartup >= startDelay) 
        {
            musicStartTime = Time.realtimeSinceStartup; //Log when music starts playing
            audioSource.Play(); //TEMP: Play audio clip
        }

        if (audioSource.isPlaying == true) timeSinceBeat += Time.deltaTime; //Increment beat time
        if (timeSinceBeat >= (beatsPerMinute/(240 * bouncesPerBeat)))
        {
            timeSinceBeat = 0; //Reset time since beat
            for (int x = sheebAnimators.Count; x > 0; x--)
                { sheebAnimators[x - 1].SetFloat("BounceSpeed", bounceSpeed); sheebAnimators[x - 1].SetTrigger("Beat"); }
        }

    }

    private void GetSheebs() //Populates "sheeb" list with all sheebs in scene
    {
        redSheebsInScene = GameObject.FindGameObjectsWithTag("Red_Sheeb"); //Find all red sheebs
        blueSheebsInScene = GameObject.FindGameObjectsWithTag("Blue_Sheeb"); //Find all blue sheebs
        greenSheebsInScene = GameObject.FindGameObjectsWithTag("Green_Sheeb"); //Find all green sheebs
        for (int x = redSheebsInScene.Length; x > 0; x--) { if (sheebs.Contains(redSheebsInScene[x - 1]) == false) { sheebs.Add(redSheebsInScene[x - 1]); } } //Add any new red sheebs
        for (int y = blueSheebsInScene.Length; y > 0; y--) { if (sheebs.Contains(blueSheebsInScene[y - 1]) == false) { sheebs.Add(blueSheebsInScene[y - 1]); } } //Add any new red sheebs
        for (int z = greenSheebsInScene.Length; z > 0; z--) { if (sheebs.Contains(greenSheebsInScene[z - 1]) == false) { sheebs.Add(greenSheebsInScene[z - 1]); } } //Add any new red sheebs

        sheebAnimators.Clear(); //Clear animator list to prevent doubles
        for (int x = sheebs.Count; x > 0; x--)
        { if (sheebs[x - 1].GetComponentInChildren<Animator>() != null) sheebAnimators.Add(sheebs[x - 1].GetComponentInChildren<Animator>()); } //Get each existing animator and add to list
    }
}
