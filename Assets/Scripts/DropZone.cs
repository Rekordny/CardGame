using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop " + eventData.pointerDrag.name + " to "+ gameObject.name);
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if(draggable != null)
        {
            draggable.canvasParent = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("PointerIn " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("PointerOut " + gameObject.name);
    }
}
