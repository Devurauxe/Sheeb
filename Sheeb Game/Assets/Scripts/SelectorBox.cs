using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorBox : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    public static HashSet<SelectorBox> allMySelectables = new HashSet<SelectorBox>();
    public static HashSet<SelectorBox> currentlySelected = new HashSet<SelectorBox>();

    Renderer sheebRenderer;

    [SerializeField]
    Material unselectedMaterial;
    [SerializeField]
    Material selectedMaterial;

    private void Awake()
    {
        allMySelectables.Add(this);
        sheebRenderer = GetComponent<Renderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        currentlySelected.Add(this);
        sheebRenderer.material = selectedMaterial;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        sheebRenderer.material = unselectedMaterial;
    }

    public static void DeselectAll(BaseEventData eventData)
    {
        foreach(SelectorBox selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }
}
