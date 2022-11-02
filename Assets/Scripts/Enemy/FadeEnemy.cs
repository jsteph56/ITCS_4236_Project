using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnemy : MonoBehaviour
{
    private bool fadeOut, fadeIn;
    public float fadeSpeed;

    void Update()
    {
        
    }

    public IEnumerator FadeOutObject()
    {
        while (this.GetComponent<SpriteRenderer>().material.color.a > 0)
        {
            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeInObject()
    {
        while (this.GetComponent<SpriteRenderer>().material.color.a < 1)
        {
            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;
            yield return null;
        }
    }
}
