using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // O método ShakeScreen() utiliza um loop para mover a posição da câmera em uma direção aleatória seis vezes,
    // com um intervalo de espera de 0,05 segundos entre cada movimento. Após isso, a câmera é reposicionada para a posição original.
    // Este método é utilizado para criar o tremor de tela.
    public static IEnumerator ShakeScreen()
    {
        for (int i = 0; i < 6; i++)
        {
            Camera.main.transform.position = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Camera.main.transform.position.z);
            yield return new WaitForSeconds(0.05f);
        }
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        yield return null;
    }
}
