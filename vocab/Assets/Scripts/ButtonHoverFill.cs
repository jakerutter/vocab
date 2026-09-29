using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverFill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image hoverFill;
    [SerializeField] private float fillDuration = 1f;

    private float targetFill;

    private void Awake()
    {
        hoverFill.fillAmount = 0f;
    }

    private void Update()
    {
        float speed = 1f / fillDuration;

        hoverFill.fillAmount = Mathf.MoveTowards(
            hoverFill.fillAmount,
            targetFill,
            speed * Time.unscaledDeltaTime
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetFill = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetFill = 0f;
    }
}