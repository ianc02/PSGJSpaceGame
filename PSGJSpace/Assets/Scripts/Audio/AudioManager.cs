using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public float masterVolume = 1;
    [Range(0f, 1f)]
    public float musicVolume = 1;
    [Range(0f, 1f)]
    public float SFXVolume = 1;
    [Range(0f, 1f)]

    private Bus masterBus;
    private Bus musicBus;
    private Bus SFXBus;


    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    private EventInstance boost;
    private bool initialized = false;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple AudioManagers in Scene.");

        }
        instance = this;
        eventInstances = new List<EventInstance>();
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/musc");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);    
        SFXBus.setVolume(SFXVolume);
    }
    public void InitializeMusic(EventReference musicEventReference)
    {
        if (!initialized)
        {
            musicEventInstance = CreateEventInstance(musicEventReference);
            musicEventInstance.start();
            boost = CreateEventInstance(FMODEvents.instance.Boost);
            initialized = true;
        }
    }
    public void PlayOneShot(EventReference sound, Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound, position);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    private void setGainParameter (string parameterName, float value)
    {
        boost.setParameterByName(parameterName, value);
    }

    
}
