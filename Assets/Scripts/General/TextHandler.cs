using UnityEngine;
using TMPro;
using System.Collections;

public class TextHandler : MonoBehaviour
{
    private float fadeDurationRate = 0.175f;
    private Coroutine activeFadeCoroutine;

    public void HandleTextAppearing(TextMeshProUGUI textMesh)
    {
        if(activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
        }

        activeFadeCoroutine = StartCoroutine(FadeIn(textMesh));

        /*Color newTextColor = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 100f);

        textMesh.color = Color.Lerp(textMesh.color, newTextColor, fadeDurationRate);*/
    }

    public void HandleTextDisappering(TextMeshProUGUI textMesh)
    {
        if(activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
        }

        activeFadeCoroutine = StartCoroutine(FadeOut(textMesh));

        /*Color newTextColor = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);

        textMesh.color = Color.Lerp(textMesh.color, newTextColor, fadeDurationRate);*/
    }

    private IEnumerator FadeIn(TextMeshProUGUI textMesh)
    {
        Color originalColor = textMesh.color;

        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float elapsedTime = 0f;

        while(elapsedTime < fadeDurationRate)
        {
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDurationRate);

            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        activeFadeCoroutine = null;
    }

    private IEnumerator FadeOut(TextMeshProUGUI textMesh)
    {
        Color originalColor = textMesh.color;

        float startAlpha = originalColor.a;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDurationRate)
        {
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDurationRate);

            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        activeFadeCoroutine = null;
    }
}
