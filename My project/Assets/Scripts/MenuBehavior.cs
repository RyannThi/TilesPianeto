using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public static bool submenu;
    public static string packName;
    private GameObject menuMain;
    private GameObject menuStore;

    // Start is called before the first frame update
    void Start()
    {
        menuMain = GetChildGameObject(gameObject, "MainSection");
        menuStore = GetChildGameObject(gameObject, "StoreSection");
        packName = "Japan";
    }

    // Update is called once per frame
    void Update()
    {
        if (submenu)
        {
            menuMain.SetActive(false);
            menuStore.SetActive(true);
        } else
        {
            menuMain.SetActive(true);
            menuStore.SetActive(false);
        }
    }

    // Função para pegar objetos com nomes específicos que são filhos do objeto com o script anexado
    public GameObject GetChildGameObject(GameObject _fromGameObject, string _withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = _fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == _withName) return t.gameObject;
        return null;
    }
}
