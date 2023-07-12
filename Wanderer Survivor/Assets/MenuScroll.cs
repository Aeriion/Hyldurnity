using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    private bool isDragging = false;
    private Vector2 dragStartPos;

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
            scrollRect.verticalNormalizedPosition += differenceY * 0.01f;
            dragStartPos = dragEndPos;
        }
    }
}