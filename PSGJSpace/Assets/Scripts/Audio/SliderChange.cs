using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{

    public string path;
    // Start is called before the first frame update
    public void SliderValChange()
    {
        if (path == "master")
        {
            AudioManager.instance.masterVolume= gameObject.GetComponent<Slider>().value;
        }
        else if (path == "musc")
        {
            AudioManager.instance.musicVolume = gameObject.GetComponent<Slider>().value;
        }
        else if (path == "sfx")
        {
            AudioManager.instance.SFXVolume = gameObject.GetComponent<Slider>().value;
        }
        else
        {
            Debug.Log(path);
        }
    }

}
