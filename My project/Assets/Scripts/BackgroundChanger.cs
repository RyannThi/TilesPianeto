using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public List<ImagePack> imagePacks;
    public Sprite[] spriteList;

    private GameObject staticBackground, scroller1, scroller2, scroller3;

    // Start is called before the first frame update
    void Start()
    {
        staticBackground = GetChildGameObject(gameObject, "BackgroundStatic");
        scroller1 = GetChildGameObject(gameObject, "BackgroundScroll");
        scroller2 = GetChildGameObject(gameObject, "BackgroundScroll (1)");
        scroller3 = GetChildGameObject(gameObject, "BackgroundScroll (2)");

        foreach (ImagePack pack in imagePacks)
        {
            if (pack.packName == MenuBehavior.packName)
            {
                spriteList = pack.images;
                break;
            }
        }

        staticBackground.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        scroller1.GetComponent<SpriteRenderer>().sprite = spriteList[1];
        scroller2.GetComponent<SpriteRenderer>().sprite = spriteList[1];
        scroller3.GetComponent<SpriteRenderer>().sprite = spriteList[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetChildGameObject(GameObject _fromGameObject, string _withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = _fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == _withName) return t.gameObject;
        return null;
    }
}
