using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Material transitionMaterial; // Assign the material with the shader
    public float duration = 15f;

    private float cutoff = 0;

    void Start()
    {
        StartCoroutine(TransitionIn());
    }

    IEnumerator TransitionIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cutoff = Mathf.Lerp(0, 1, elapsedTime / duration);
            transitionMaterial.SetFloat("_Cutoff", cutoff);
            yield return null;
        }

        transitionMaterial.SetFloat("_Cutoff", 1);
    }
}
