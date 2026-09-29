using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    public Image image;

    public void FadeIn()
    {
        StartCoroutine(FadeImageAlpha(2f, 1f));
    }

    private IEnumerator FadeImageAlpha(float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        Color color = image.color;

        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                startAlpha,
                1f,
                elapsed / duration
            );

            image.color = color;

            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }
}