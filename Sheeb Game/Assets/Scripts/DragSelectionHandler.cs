using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSelectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    //Still do not know what serializefield is but variable for the selection box
    [SerializeField]
    Image selectionBoxImage;

    //variables for the math regarding the selection box
    Vector2 startPosition;
    Rect selectionRect;

    //When it starts dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //if control is not being held deselect all that were selected
        if(!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            SelectorBox.DeselectAll(new BaseEventData(EventSystem.current));
        }

        //set the selection box active, set the startPosition to where the mouse is currently at and create a new rectangle
        selectionBoxImage.gameObject.SetActive(true);
        startPosition = eventData.position;
        selectionRect = new Rect();
    }

    //when being dragged
    public void OnDrag(PointerEventData eventData)
    {
        //if the mouse cursors x position is below the starter position, invert the value of the rectangles area, if not do nnot invert
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

        //if the mouse cursors x position is below the starter position, invert the value of the rectangles area, if not do nnot invert
        if (eventData.position.y < startPosition.y)
        {
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPosition.y;
        }
        else
        {
            selectionRect.yMin = startPosition.y;
            selectionRect.yMax = eventData.position.y;
        }

        //set the minimum and the maximum of the rectangle to the selection rectangle to the minimum and the maximum
        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
    }

    //When you stop dragging the box
    public void OnEndDrag(PointerEventData eventData)
    {
        //sets the selection box to false
        selectionBoxImage.gameObject.SetActive(false);

        //for every selectable in the list allMySelectables
        foreach(SelectorBox selectable in SelectorBox.allMySelectables)
        {
            //no idea what the if statement is all about but when the select box is over the sheebs, change them to selected
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(selectable.transform.position)))
            {
                selectable.OnSelect(eventData);
            }
        }
    }

    //Only here to bubble in the functionality of the pointerclick, other than that I have no idea how this part of the code works
    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        float distance = 0;

        foreach(RaycastResult result in results)
        {
            if(result.gameObject == gameObject)
            {
                distance = result.distance;
                break;
            }
        }

        GameObject nextObject = null;
        float maxDistance = Mathf.Infinity;

        foreach(RaycastResult result in results)
        {
            if (result.distance > distance && result.distance < maxDistance)
            {
                nextObject = result.gameObject;
                maxDistance = result.distance;
            }
        }

        if (nextObject)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (x, y) => { x.OnPointerClick((PointerEventData)y); });
        }
    }
}
