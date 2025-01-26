using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float totalTime;
    public float alphaChangePerFrame;
    private float startTime;
    private bool alphaIncrease;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        alphaIncrease = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (totalTime - (Time.time - startTime) < 3)

        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            if (alphaIncrease)
            {
                color.a = color.a + alphaChangePerFrame;
            }
            else
            {
                color.a = color.a - alphaChangePerFrame;
            }
            if ((color.a < 0.01 && !alphaIncrease) || (color.a > 0.99 && alphaIncrease))
            {
                alphaIncrease = ! alphaIncrease;
            }
            
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
        if (Time.time - startTime > totalTime)
        {

            Destroy(gameObject);
        }

    }
}
