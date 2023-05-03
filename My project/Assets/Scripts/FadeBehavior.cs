using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBehavior : MonoBehaviour
{
    public bool fadeIn;
    private Image selfImage;
    private Color selfColor;

    // Start is called before the first frame update
    void Start()
    {
        selfImage = GetComponent<Image>();
        selfColor = GetComponent<Image>().color;
        StartCoroutine(Fading());
    }

    // Update is called once per frame
    IEnumerator Fading()
    {
        if (fadeIn)
        {
            for (int i = 0; selfColor.a != 1f; i++)
            {
                selfColor.a = Mathf.Lerp(selfColor.a, 1, Time.deltaTime * 1f);
                selfImage.color = selfColor;
                yield return null;
            }
        } else
        {
            for (int i = 1; selfColor.a != 0f; i--)
            {
                selfColor.a = Mathf.Lerp(selfColor.a, 0, Time.deltaTime * 1f);
                selfImage.color = selfColor;
                yield return null;
            }
        }
    }
}
