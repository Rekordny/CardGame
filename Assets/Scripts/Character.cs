
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string characterName;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _attack;
    [SerializeField]
    private float _defense;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]//血条图片
    private Image hpBar;
    [SerializeField]
    public Animator animatorReference;
    public Character Instance;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float Health
    {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            HpBarUpdate();
            if (Health == 0)
                CharacterDie();
        }
    }

    public float Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    public float Defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    public CharacterData characterData;

    private void Awake()
    {
        animatorReference = GetComponentInChildren<Animator>();
        Instance = this;
    }


    private void Start()
    {
        Initialize(characterData);
        // agentSpeed SETUP
        gameObject.GetComponent<NavMeshAgent>().speed = _moveSpeed;
        hpBar = transform.Find("Visual/HealthBar/hp").GetComponent<Image>();
    }

    public void Initialize(CharacterData characterData)
    {
        // 把数据拿进来mono behavior
        _attack = characterData.attack;
        _defense = characterData.defense;
        _moveSpeed = characterData.moveSpeed;
        _attackRange = characterData.attackRange;
        _attackSpeed = characterData.attackSpeed;
        _maxHealth = characterData.maxHealth;
        characterName = characterData.name;

        //默认初始满血
        _health = _maxHealth;
        // 获得Prefab的Reference并且更新prefab reference再加上视觉的text,卧槽原始人

        //设定navMesh
        GetComponent<NavMeshAgent>().speed = MoveSpeed;
    }

    public void HpBarUpdate()
    {
        //Debug.Log("Change to %"+ (float)Health / (float)MaxHealth);
        hpBar.fillAmount = (float)Health/ (float)MaxHealth;
    }

    // 执行向目标坐标移动
    public void MoveTo(Vector3 target)
    {
        this.SetTriggerAnimation(Utils.walkingParam);
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    // 执行一次攻击/治疗，治疗为负数
    public void AttackCharacter(Character target)
    {
        Debug.Log("Attck once to "+target.characterName+" Distance: "+Vector3.Distance(target.transform.position, this.transform.position));
        this.SetTriggerAnimation(Utils.attackingParam);
        if (Attack >= 0)
        {
            target.TakeDamage(this.Attack);
        }
        else
        {
            target.TakeHeal(this.Attack);
        }
    }

    // 被伤害
    public void TakeDamage(float damage)
    {
        //Debug.Log(this.characterName+" is taking "+damage);
        float result = Mathf.Clamp(damage - this.Defense, 0f, 999f);
        Health -= result;
        FindAnyObjectByType<DamagePopout>().ShowDamagePopout(result, this);
    }

    // 吃奶
    public void TakeHeal(float healVar)
    {
        //因为回血是负数
        Health -= healVar;
        FindAnyObjectByType<DamagePopout>().ShowDamagePopout(healVar, this);
    }

    public void ToIdle()
    {
        SetTriggerAnimation(Utils.idlingParam, true);
    }


    // 死亡
    public void CharacterDie()
    {
        CharacterManager.Instance.RemoveCharacter(this);
        Debug.Log("Remove "+this.characterName);
        StartCoroutine(CharacterDieCorotine());
    }

    IEnumerator CharacterDieCorotine()
    {
        // 播放动画
        SetTriggerAnimation(Utils.dyingParam);

        // 等待动画播放完毕
        while (animatorReference.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // 动画播放完毕后执行操作
        Destroy(gameObject);
    }

    public void HighLightMe(int num)
    {
        if(null != this)
        {
            switch(num)
            {
                case 0:
                    GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    break;
                case 1:
                    GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    break;
                case 2:
                    GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    break;
                default: break;
            }
        }
    }

    public void SetTriggerAnimation(int param, bool state)
    {
        // Reset other triggers
        foreach (int paramToCheck in Utils.paramList)
        {
            if (!paramToCheck.Equals(param))
                animatorReference.SetBool(param, false);
        }

        if (animatorReference.GetBool(param) != state)
        {
            animatorReference.SetBool(param, state);
        }
    }
    public void SetTriggerAnimation(int param)
    {
        // Reset other triggers
        foreach (int paramToCheck in Utils.paramList)
        {
            if (!paramToCheck.Equals(param))
                animatorReference.SetBool(paramToCheck, false);
        }

        if (animatorReference.GetBool(param) != true)
        {
            animatorReference.SetBool(param, true);
        }
    }

    public bool Healable()
    {
        return Health < MaxHealth;
    }

}
