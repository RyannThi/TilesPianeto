using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class ImagePack
{
    public string packName;
    public Sprite[] images;
}

public class TeclasPool : MonoBehaviour
{
    private static float teclaVelocidade = 3;
    public GameObject prefab; // Prefab do objeto que será instanciado
    public int initialPoolSize; // Tamanho inicial da pool
    public bool canGrow; // Indica se a pool pode crescer dinamicamente
    private static float[] positionBlocks = { -1.5f, 0, 1.5f }; // Posições possíveis no eixo x para as notas
    public static float interval = 2f; // Intervalo entre as notas
    private float timeSpent = 0f;

    //public Sprite[] spriteList; // Imagens para as notas
    public List<ImagePack> imagePacks;

    public static Sprite[] spriteListStatic; // Imagens para as notas estaticas
    public static Queue<GameObject> objectPool; // Fila para armazenar os objetos da pool
    Coroutine shakeCoroutine;

    void Start()
    {
        foreach (ImagePack pack in imagePacks)
        {
            if (pack.packName == MenuBehavior.packName)
            {
                spriteListStatic = pack.images;
                break;
            }
        }

        // Inicializa a pool
        objectPool = new Queue<GameObject>();
        //spriteListStatic = spriteList;
        shakeCoroutine = StartCoroutine(EmptyCoroutine());

        // Cria objetos e adiciona na pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            // Instancia um objeto
            GameObject obj = Instantiate(prefab, new Vector3(positionBlocks[Random.Range(0, 3)], 4.45f, transform.position.z), Quaternion.identity, transform);

            // Adiciona um número ao nome do objeto
            obj.name = obj.name + i;

            // Desativa o objeto
            obj.SetActive(false);

            // Adiciona o objeto à pool
            objectPool.Enqueue(obj);
        }
    }

    void Update()
    {
        timeSpent += Time.deltaTime;

        // Verifica se o tempo decorrido é maior ou igual ao intervalo desejado
        if (timeSpent >= interval)
        {
            // Cria um novo objeto a partir do prefab
            var newObj = GetObject();
            newObj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -teclaVelocidade, 0);

            // Reinicia o tempo decorrido
            timeSpent = 0f;
        }

        // Verifica se os objetos saíram fora de campo e os desativa, reseta e coloca de volta na pool
        foreach (Transform child in transform)
        {
            var obj = child.gameObject;
            if (obj.activeSelf && obj.transform.position.y < -4.45f)
            {
                obj.transform.position = new Vector3(positionBlocks[Random.Range(0, 3)], 4.45f, 0f); // Reposiciona o objeto
                obj.SetActive(false); // Desativa o objeto
                objectPool.Enqueue(obj); // Coloca o objeto de volta no final da fila

                // Reseta a velocidade/intervalo/combo das notas
                UILabels.ResetComboValue();
                teclaVelocidade = 3;
                interval = 2;

                // Treme a tela
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = StartCoroutine(CameraShake.ShakeScreen()); 
            }
        }
    }

    // Função para obter um objeto da pool
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            obj.GetComponent<SpriteRenderer>().sprite = spriteListStatic[Random.Range(0, spriteListStatic.Length)];
            return obj;
        }
        else if (canGrow)
        {
            GameObject obj = Instantiate(prefab);
            return obj;
        }
        else
        {
            return null; // Retorna null se a pool estiver vazia e não puder crescer
        }
    }

    // Função para devolver um objeto à pool
    public static void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        obj.transform.position = new Vector3(TeclasPool.positionBlocks[Random.Range(0, 3)], 4.45f, 0f); // Reposiciona o objeto
        
        // Troca o gráfico das notas
        obj.GetComponent<SpriteRenderer>().sprite = TeclasPool.spriteListStatic[Random.Range(0, TeclasPool.spriteListStatic.Length)];

        objectPool.Enqueue(obj);
        
        // Diminui o intervalo e aumenta a velocidade das notas, limitando-as
        if (TeclasPool.interval > 0.4f)
        {
            TeclasPool.interval -= 0.07f;
        }
        if (TeclasPool.teclaVelocidade < 7f)
        {
            TeclasPool.teclaVelocidade += 0.3f;
        }
    }

    private IEnumerator EmptyCoroutine()
    {
        yield return null;
    }
}