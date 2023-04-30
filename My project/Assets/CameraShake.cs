using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
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
