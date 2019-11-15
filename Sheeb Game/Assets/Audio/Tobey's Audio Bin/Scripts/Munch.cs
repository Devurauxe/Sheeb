using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munch : MonoBehaviour
{

    [FMODUnity.EventRef] //Event Caller
    public string soundInput;
    FMOD.Studio.EventInstance Munch_Eat;
    public bool eating;

    // Start is called before the first frame update
    void Start()
    {
        // FMOD Starter
        Munch_Eat = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Eating");
        Munch_Eat.start();
        eating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(eating == false)
        {
            Munch_Eat.setParameterByName("Munch Eat", 0);
        } else if(eating == true)
            {
            Munch_Eat.setParameterByName("Munch Eat", 1);
            }
    }
}
