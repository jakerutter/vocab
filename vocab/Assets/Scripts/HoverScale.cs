using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scalePercent = 10f;
    [SerializeField] private float speed = 8f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool disabledStateApplied;
    private Button button;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (button != null && !button.interactable)
        {
            if (!disabledStateApplied)
            {
                //isHovered = false;
                targetScale = originalScale;
                transform.localScale = originalScale;
                disabledStateApplied = true;
            }

            return;
        }

        disabledStateApplied = false;

        if (Vector3.SqrMagnitude(transform.localScale - targetScale) < 0.001f)
        {
            transform.localScale = targetScale;
            return;
        }

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