using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeclasPress : MonoBehaviour
{
    private SpriteRenderer selfSprite;
    private GameObject tecla;

    // Start is called before the first frame update
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && gameObject.name == "Square_Middle")
        {
            DetectNote(selfSprite, tecla);
        }

        if (Input.GetKey(KeyCode.DownArrow) && gameObject.name == "Square_Middle")
        {
            DetectNote(selfSprite, tecla);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.name == "Square_Left")
        {
            DetectNote(selfSprite, tecla);
        }

        if (Input.GetKey(KeyCode.RightArrow) && gameObject.name == "Square_Right")
        {
            DetectNote(selfSprite, tecla);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && gameObject.name == "Square_Middle")
        {
            selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && gameObject.name == "Square_Middle")
        {
            selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && gameObject.name == "Square_Left")
        {
            selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && gameObject.name == "Square_Right")
        {
            selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            selfSprite.color = new Color(1f, 1f, 1f, 1f);
            if (tecla)
            {
                selfSprite.color = new Color(0.7f, 1f, 0.7f, 1f);
                TeclasPool.ReturnObject(tecla);
            }
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0)) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }

    private void DetectNote(SpriteRenderer selfSprite, GameObject tecla)
    {
        selfSprite.color = new Color(1f, 1f, 1f, 1f);
        if (tecla)
        {
            selfSprite.color = new Color(0.7f, 1f, 0.7f, 1f);
            TeclasPool.ReturnObject(tecla);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tecla"))
        {
            tecla = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Tecla"))
        {
            tecla = null;
        }
    }
}
