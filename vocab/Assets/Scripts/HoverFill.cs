using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverFill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform hoverFill;
    [SerializeField] private float fillDuration = 1f;

    private RectTransform buttonRect;
    private Button button;
    private bool isHovered;
    private bool disabledStateApplied;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonRect = transform as RectTransform;

        hoverFill.anchorMin = new Vector2(0f, 0f);
        hoverFill.anchorMax = new Vector2(0f, 1f);
        hoverFill.pivot = new Vector2(0f, 0.5f);

        hoverFill.anchoredPosition = Vector2.zero;

        hoverFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            0f
        );
    }

    private void Update()
    {
        if (button != null && !button.interactable)
        {
            if (!disabledStateApplied)
            {
                isHovered = false;

                hoverFill.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    0f
                );

                disabledStateApplied = true;
            }

            return;
        }

        disabledStateApplied = false;

        float fullWidth = buttonRect.rect.width;
        float targetWidth = isHovered ? fullWidth : 0f;
        float currentWidth = hoverFill.rect.width;

        if (Mathf.Approximately(currentWidth, targetWidth))
            return;

        float speed = fullWidth / fillDuration;

        float newWidth = Mathf.MoveTowards(
            currentWidth,
            targetWidth,
            speed * Time.unscaledDeltaTime
        );

        hoverFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            newWidth
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}