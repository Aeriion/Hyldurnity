using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.01f;
    public float deceleration = 0.1f;

    private bool isDragging = false;
    private Vector2 dragStartPos;
    private float velocity = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void LateUpdate()
    {
        if (isDragging)
        {
            Vector2 dragEndPos = Input.mousePosition;
            float differenceY = dragEndPos.y - dragStartPos.y;

            // Inversion du signe de la différence de position de la souris pour inverser l'effet de scroll
            differenceY = -differenceY;

            // Mise à jour de la vitesse en fonction de la différence de position inversée de la souris
            velocity = differenceY * scrollSpeed;

            // Applique la vitesse au défilement vertical
            scrollRect.verticalNormalizedPosition += velocity;

            dragStartPos = dragEndPos;
        }
        else if (Mathf.Abs(velocity) > 0.01f)
        {
            // Décélération progressive lorsque le défilement s'arrête
            velocity *= (1f - deceleration);

            // Applique la vitesse au défilement vertical
            scrollRect.verticalNormalizedPosition += velocity;
        }
    }
}