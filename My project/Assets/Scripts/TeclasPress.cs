using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeclasPress : MonoBehaviour
{
    private SpriteRenderer selfSprite; // Imagem do proprio obj
    private GameObject tecla; // Notas musicais que caem
    public GameObject popup; // Pop-up de aviso/rank
    private Coroutine popupCoroutine;

    private Vector2 popup_velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
        popupCoroutine = StartCoroutine(EmptyCoroutine());
        popup.GetComponent<TextMeshPro>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        string objectName = gameObject.name;

        // Alterar a cor do sprite
        if (Input.GetKeyUp(KeyCode.UpArrow)   || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (objectName == "Square_Middle" || objectName == "Square_Left" || objectName == "Square_Right")
            {
                selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
            }
        }

        // --------------------------------------------------------------------------
        
        if (Input.GetKey(KeyCode.UpArrow)    && gameObject.name == "Square_Middle") { DetectNote(selfSprite, tecla); }
        if (Input.GetKey(KeyCode.DownArrow)  && gameObject.name == "Square_Middle") { DetectNote(selfSprite, tecla); }
        if (Input.GetKey(KeyCode.LeftArrow)  && gameObject.name == "Square_Left")   { DetectNote(selfSprite, tecla); }
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.name == "Square_Right")  { DetectNote(selfSprite, tecla); }
    }

    private void OnMouseDown()
    {
        DetectNote(selfSprite, tecla);
    }

    private void OnMouseUp()
    {
        selfSprite.color = new Color(1f, 1f, 1f, 0.4f);
    }

    private void DetectNote(SpriteRenderer selfSprite, GameObject tecla)
    {
        selfSprite.color = new Color(1f, 1f, 1f, 1f);
        if (tecla)
        {
            // Calcula a distancia da nota musical com o botão e avisa o jogador o quão bom foi o "ritmo"
            var dist = Vector3.Distance(gameObject.transform.position, tecla.transform.position);
            Debug.Log(dist);
            switch (dist)
            {
                case float n when n >= 0.8f:

                    Debug.Log("Poor");
                    StopCoroutine(popupCoroutine);
                    popupCoroutine = StartCoroutine(PopupBehavior("Poor"));
                    break;

                case float n when n >= 0.6f:

                    Debug.Log("OK");
                    StopCoroutine(popupCoroutine);
                    popupCoroutine = StartCoroutine(PopupBehavior("OK"));
                    break;

                case float n when n >= 0.4f:

                    Debug.Log("Good");
                    StopCoroutine(popupCoroutine);
                    popupCoroutine = StartCoroutine(PopupBehavior("Good"));
                    break;

                case float n when n >= 0.2f:

                    Debug.Log("Great");
                    StopCoroutine(popupCoroutine);
                    popupCoroutine = StartCoroutine(PopupBehavior("Great"));
                    break;

                case float n when n >= 0f:

                    Debug.Log("Perfect!");
                    StopCoroutine(popupCoroutine);
                    popupCoroutine = StartCoroutine(PopupBehavior("Perfect!"));
                    break;
            }
            selfSprite.color = new Color(0.7f, 1f, 0.7f, 1f);
            UILabels.AddComboValue();
            TeclasPool.ReturnObject(tecla);
        }
    }

    IEnumerator PopupBehavior(string _matchType)
    {
        switch (_matchType)
        {
            case "Poor":

                popup.GetComponent<TextMeshPro>().text = "Bad";
                popup.GetComponent<TextMeshPro>().color = new Color(0.60392f, 0.37647f, 0.20000f, 0);
                popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.8f);
                break;

            case "OK":

                popup.GetComponent<TextMeshPro>().text = "OK";
                popup.GetComponent<TextMeshPro>().color = new Color(0.66275f, 0.81176f, 0.35686f, 0);
                popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.8f);
                break;

            case "Good":

                popup.GetComponent<TextMeshPro>().text = "Good";
                popup.GetComponent<TextMeshPro>().color = new Color(0.28235f, 0.83922f, 0.38039f, 0);
                popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.8f);
                break;

            case "Great":

                popup.GetComponent<TextMeshPro>().text = "Great";
                popup.GetComponent<TextMeshPro>().color = new Color(0.38431f, 0.93725f, 0.81569f, 0);
                popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.8f);
                break;

            case "Perfect!":

                popup.GetComponent<TextMeshPro>().text = "Perfect!";
                popup.GetComponent<TextMeshPro>().color = new Color(0.91765f, 0.85882f, 0.54902f, 0);
                popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.8f);
                break;
        }

        var popUpColor = popup.GetComponent<TextMeshPro>().color;
        for (float i = 0; i < 1;)
        {
            popup.GetComponent<RectTransform>().anchoredPosition = Vector2.SmoothDamp(popup.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 1f), ref popup_velocity, 0.25f);
            popUpColor.a = Mathf.Lerp(popup.GetComponent<TextMeshPro>().color.a, 0.8f, i);
            popup.GetComponent<TextMeshPro>().color = popUpColor;
            i += 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; popUpColor.a != 0f; )
        {
            popUpColor.a = Mathf.Lerp(popup.GetComponent<TextMeshPro>().color.a, 0f, i);
            popup.GetComponent<TextMeshPro>().color = popUpColor;
            i += 0.025f;
            yield return null;
        }
        yield return null;
    }

    IEnumerator EmptyCoroutine()
    {
        yield return null;
    }

    // Detecta quando uma nota entra em contato com o botão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tecla"))
        {
            tecla = collision.gameObject;
        }
    }

    // Detecta quando uma nota sai de contato com o botão
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Tecla"))
        {
            tecla = null;
        }
    }
}
