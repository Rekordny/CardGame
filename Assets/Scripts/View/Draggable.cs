using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static CardData;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _canvasParent;
    private GameObject effectRangeReference;
    private Card card;
    List<Character> targets;

    private void Awake()
    {

    }

    private void Start()
    {
        targets = new List<Character>();
        card = GetComponent<Card>();
    }

    public Transform canvasParent
    {
        get { return _canvasParent; }
        set
        {
            _canvasParent = value;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 手牌的拖拽区域
        canvasParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        StartDrawRange(card.effectRange);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;
        ChangeCardAlpha(0.15f);
        Utils.ChangeGlobalTimeScale(0.25f);
        targets = GetTargets(card.cardEffectType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        this.transform.SetParent(canvasParent);
        // 拖拽结束的时候恢复卡牌raycast
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        ChangeCardAlpha(1f);
        Utils.ChangeGlobalTimeScale(1f);
        //释放卡牌效果如果有目标可以释放
        if (targets.Count != 0 && targets != null)
        {
            card.applyEffectToCharacterList(targets);
            card.useThisCard();
        }
        //销毁范围
        if(card.cardEffectType == CardEffectType.ENTITY)
        {
            LeaveEntity();
        }
            
        Destroy(effectRangeReference);
    }

    // 根据card effectRange画圈
    public void StartDrawRange(float range)
    {
        effectRangeReference = new GameObject("EffectRange");
        effectRangeReference.AddComponent<MeshFilter>();
        effectRangeReference.AddComponent<MeshRenderer>();
        effectRangeReference.AddComponent<EffectRangeDrawer>();
        effectRangeReference.GetComponent<EffectRangeDrawer>().circleRadius = range;
    }

    public void ChangeCardAlpha(float alphaF)
    {
        Mathf.Clamp(alphaF, 0.1f, 1f);
        GetComponent<CanvasGroup>().alpha = alphaF;
    }

    // 返回所有目标对象的人物数组
    public List<Character> GetTargets(CardData.CardEffectType cardEffectType)
    {
        // 分好不同类型不同施放效果
        switch (cardEffectType)
        {
            case CardEffectType.SPELLAOE:
            case CardEffectType.BUFFAOE:
                targets = effectRangeReference.GetComponent<EffectRangeDrawer>().GetCollideCharacters();
                break;

            case CardEffectType.SPELLSINGLE:
            case CardEffectType.BUFFSINGLE:
                targets = effectRangeReference.GetComponent<EffectRangeDrawer>().GetHoverCharacter();
                break;

            case CardEffectType.ENTITY:
                targets = effectRangeReference.GetComponent<EffectRangeDrawer>().GetCollideCharacters();
                break;
            case CardEffectType.NONUSABLE:
                break;
        }

        return targets;
    }

    // 添加障碍
    private GameObject LeaveEntity()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = effectRangeReference.transform.position;
        sphere.transform.localScale = new Vector3(card.effectRange * 2, card.effectRange * 2, card.effectRange * 2);
        sphere.AddComponent<NavMeshObstacle>().carving = true;
        return sphere;
    }




}
