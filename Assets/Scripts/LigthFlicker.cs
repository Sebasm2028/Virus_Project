using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    private Light pointLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}