using UnityEngine;

public class RemoveScripts : MonoBehaviour
{
    void Start()
    {
        var scripts = FindObjectsOfType<SimpleRandom>();
        foreach (var script in scripts)
        {
            DestroyImmediate(script);
        }
    }
}