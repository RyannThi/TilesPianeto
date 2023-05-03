using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILabels : MonoBehaviour
{
    private GameObject comboLabel; // Texto "Combo:" na interface
    public static TextMeshProUGUI comboLabelValue; // Valor do combo na interface
    private GameObject highLabel; // Texto "Highest Combo:" na interface
    public static TextMeshProUGUI highLabelValue; // Valor do maior combo na interface
    private GameObject coinsLabel; // Texto "Coins:" na interface
    public static TextMeshProUGUI coinsLabelValue; // Valor de moedas na interface

    public static int comboValue; // Valor do combo
    public static int highValue; // Valor do maior combo

    void Start()
    {
        // Associa as variaveis com os objetos na cena
        comboLabel = GetChildGameObject(gameObject, "ComboLabel");
        comboLabelValue = GetChildGameObject(gameObject, "ComboLabelValue").GetComponent<TextMeshProUGUI>();
        highLabel = GetChildGameObject(gameObject, "HighLabel");
        highLabelValue = GetChildGameObject(gameObject, "HighLabelValue").GetComponent<TextMeshProUGUI>();
        coinsLabel = GetChildGameObject(gameObject, "CoinsLabel");
        coinsLabelValue = GetChildGameObject(gameObject, "CoinsLabelValue").GetComponent<TextMeshProUGUI>();

        comboValue = 0;
        highValue = PlayerPrefs.GetInt("HIGHVALUE", 0);
        coinsLabelValue.text = PlayerPrefs.GetInt("COINS", 0).ToString();
        comboLabelValue.text = comboValue.ToString();
    }

    void Update()
    {
        
    }

    public static void AddComboValue(bool _isPerfect = false)
    {
        // Aumenta o valor do combo. Se a nota acertada for perfeita, aumente em 2
        comboValue += 1;
        if (_isPerfect)
        {
            comboValue += 1;
        }

        // Se o valor do combo atual for maior que o highest combo, torne o highest combo no combo atual
        if (comboValue > highValue)
        {
            highValue = comboValue;
            highLabelValue.text = highValue.ToString();
        }

        // Altera o valor do combo na interface
        comboLabelValue.text = comboValue.ToString();

        // Caso o combo seja algum valor múltiplo de 50, adicione uma moeda
        if (comboValue % 50 == 0 && comboValue != 0)
        {
            PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + 1);
            coinsLabelValue.text = PlayerPrefs.GetInt("COINS").ToString();
        }
        
    }

    public static void ResetComboValue()
    {
        comboValue = 0;
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
