using UnityEngine;
using UnityEngine.EventSystems;

public class VerticalScroll : MonoBehaviour, IDragHandler
{
    public float scrollSpeed = 1f;
    public RectTransform scrollViewContent;
    public RectTransform scrollViewViewport;

    private float contentHeight;
    private float viewportHeight;
    private float maxContentY;
    private float minContentY;

    private void Start()
    {
        UpdateContentHeight();
        UpdateViewportHeight();
        CalculateMinMaxContentY();
    }

    private void UpdateContentHeight()
    {
        contentHeight = scrollViewContent.rect.height;
    }

    private void UpdateViewportHeight()
    {
        viewportHeight = scrollViewViewport.rect.height;
    }

    private void CalculateMinMaxContentY()
    {
        maxContentY = Mathf.Max(0f, contentHeight - viewportHeight);
        minContentY = 0f;

        if (contentHeight <= viewportHeight)
        {
            // Si la hauteur totale du contenu est plus petite ou égale à la hauteur du viewport,
            // ne pas permettre le défilement
            maxContentY = minContentY;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.delta.y;
        float newContentY = scrollViewContent.anchoredPosition.y + deltaY * scrollSpeed;

        if (newContentY > maxContentY)
        {
            newContentY = maxContentY;
        }
        else if (newContentY < minContentY)
        {
            newContentY = minContentY;
        }

        scrollViewContent.anchoredPosition = new Vector2(scrollViewContent.anchoredPosition.x, newContentY);
    }

    private void Update()
    {
        // Vérifier si la hauteur du contenu ou du viewport a changé
        if (scrollViewContent.rect.height != contentHeight || scrollViewViewport.rect.height != viewportHeight)
        {
            UpdateContentHeight();
            UpdateViewportHeight();
            CalculateMinMaxContentY();
        }
    }
}