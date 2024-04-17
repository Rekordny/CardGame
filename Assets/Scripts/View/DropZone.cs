using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform m_RectTransform;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        if(m_RectTransform)
            m_RectTransform.anchoredPosition = new Vector3(0, -100, 0);
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop " + eventData.pointerDrag.name + " to "+ gameObject.name);
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if(draggable != null)
        {
            draggable.canvasParent = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_RectTransform.anchoredPosition = Vector3.zero;
        //Debug.Log("PointerIn " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_RectTransform.anchoredPosition = new Vector3(0, -100, 0);
        //Debug.Log("PointerOut " + gameObject.name);
    }
}
