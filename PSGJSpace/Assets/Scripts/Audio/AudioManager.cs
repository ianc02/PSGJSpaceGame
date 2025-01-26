using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }


    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    private EventInstance boost;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple AudioManagers in Scene.");

        }
        instance = this;
        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
        boost = CreateEventInstance(FMODEvents.instance.Boost);
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
