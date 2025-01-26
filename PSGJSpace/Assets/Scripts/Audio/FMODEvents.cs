using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    [field: Header("Boost")]
    [field: SerializeField] public EventReference Boost { get; private set; }
    [field: Header("Explosions")]
    [field: SerializeField] public EventReference Explsions { get; private set; }
    [field: Header("Pickups")]
    [field: SerializeField] public EventReference Pickups { get; private set; }
    [field: Header("Hurt")]
    [field: SerializeField] public EventReference Hurt { get; private set; }
    [field: Header("Selection")]
    [field: SerializeField] public EventReference Selection { get; private set; }


    public static FMODEvents instance {get; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one FMOD Events instance in scene");

        }
        instance = this;
    }
}
