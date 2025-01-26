using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void SliderValChange(string path, float value)
    {
        if (path == "")
        {
            AudioManager.instance.masterVolume= ((float)value);
        }
        else if (path == "music")
        {
            AudioManager.instance.musicVolume = ((float)value);
        }
        if (path == "sfx")
        {
            AudioManager.instance.SFXVolume = ((float)value);
        }
    }

}
