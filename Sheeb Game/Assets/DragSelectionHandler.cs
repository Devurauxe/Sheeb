using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSelectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Image selectionBoxImage;

    Vector2 startPosition;
    Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            SelectorBox.DeselectAll(new BaseEventData(EventSystem.current));
        }
        selectionBoxImage.gameObject.SetActive(true);
        startPosition = eventData.position;
        selectionRect = new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position.x < startPosition.x)
        {
            selectionRect.xMin = eventData.position.x;
            selectionRect.xMax = startPosition.x;
        }
        else
        {
            selectionRect.xMin = startPosition.x;
            selectionRect.xMax = eventData.position.x;
        }

        if(eventData.position.y < startPosition.y)
        {
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPosition.y;
        }
        else
        {
            selectionRect.yMin = startPosition.y;
            selectionRect.yMax = eventData.position.y;
        }

        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBoxImage.gameObject.SetActive(false);
        foreach(SelectorBox selectable in SelectorBox.allMySelectables)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(selectable.transform.position)))
            {
                selectable.OnSelect(eventData);
            }
        }
    }
}
