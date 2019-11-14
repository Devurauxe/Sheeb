using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorBox : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    //I have literally no idea what a hashset is but beats me.
    public static HashSet<SelectorBox> allMySelectables = new HashSet<SelectorBox>();
    public static HashSet<SelectorBox> currentlySelected = new HashSet<SelectorBox>();

    //getting the renderer
    Renderer sheebRenderer;

    //Don't know what serialize field is but variables for changing color
    [SerializeField]
    Material unselectedMaterial;
    [SerializeField]
    Material selectedMaterial;

    //adds all the selected items into allMySelectables and gets renderer component ready
    private void Awake()
    {
        allMySelectables.Add(this);
        sheebRenderer = GetComponent<Renderer>();
    }
    
    //When the mouse clicks on something and control is not being held, deselect all, then select based on mouse click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        OnSelect(eventData);
    }

    //Adds the selected items to a currentlySelected list and then changes the renderer to show its been selected
    public void OnSelect(BaseEventData eventData)
    {
        currentlySelected.Add(this);
        sheebRenderer.material = selectedMaterial;
    }

    //When it is deselected change the renderer to its original material
    public void OnDeselect(BaseEventData eventData)
    {
        sheebRenderer.material = unselectedMaterial;
    }

    //When it deselects all the selectables that are currently selected goes through OnDeselect and clears the currentlySelected list
    public static void DeselectAll(BaseEventData eventData)
    {
        foreach(SelectorBox selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }
}
