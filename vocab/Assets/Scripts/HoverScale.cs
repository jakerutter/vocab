using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scalePercent = 10f;
    [SerializeField] private float speed = 8f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            speed * Time.unscaledDeltaTime
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float multiplier = 1f + (scalePercent / 100f);
        targetScale = originalScale * multiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}