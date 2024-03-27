using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum Type { SPELL, BUFF };
    public Type typeOfCard = Type.SPELL;
    private Transform _canvasParent;
    public Transform canvasParent
    {
        get { return _canvasParent; }
        set { 
            _canvasParent = value;
            Debug.Log("Set parent to "+ value.name);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        // 拖拽的时候用鼠标判定raycast到哪个区域或者目标
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(canvasParent);
        // 拖拽结束的时候恢复卡牌raycast
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
