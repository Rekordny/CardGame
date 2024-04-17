using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : Singleton<CharacterManager>
{

    public List<Character> characterList;
    public GameObject characterListReference;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        characterList = new List<Character>();
        AddChildrenToList();
    }

    private void AddChildrenToList()
    {
        for (int i = 0; i < characterListReference.transform.childCount; ++i)
        {
            characterList.Add(characterListReference.transform.GetChild(i).GetComponent<Character>());
        }
    }

    public bool IfEnemyExist(Character character)
    {
        if(character.tag == "Player")
        {
            return GameObject.FindGameObjectWithTag("Enemy");
        }
        else
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }
    
    public bool IfAllyExist(Character character)
    {
        if(character.tag == "Player")
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            return GameObject.FindGameObjectWithTag("Enemy");
        }
    }

    public bool IfHealableAllyExist(Character origin)
    {
        foreach (Character character in characterList)
        {
            if (origin.CompareTag(character.tag)&&character.Healable())
            {
                Debug.Log("HealableExist");
                return true;
            }
        }
        return false;
    }

    public Character GetClosestAlly(Character origin)
    {
        // At least have another to compare with
        if (characterList.Count > 1)
        {
            Character result = characterList[0];
            float distanceTemp = 999;
            foreach (Character character in characterList)
            {
                // I do mpt compare to myself AND I look for same tag
                if (character.gameObject != origin.gameObject && character.CompareTag(origin.gameObject.tag))
                {

                    float temp = Vector3.Distance(origin.transform.position, character.transform.position);
                    if (temp < distanceTemp)
                    {
                        distanceTemp = temp;
                        result = character;
                    }
                    //Debug.Log(distanceTemp);
                }
            }
            return result;
        }
        else
        {
            return null;
        }
    }

    public Character GetClosestHealableAlly(Character origin)
    {
        // At least have another to compare with
        if (characterList.Count > 1)
        {
            Character result = characterList[0];
            float distanceTemp = 999;
            foreach (Character character in characterList)
            {
                // I do mpt compare to myself AND I look for same tag AND I am not fullHP
                if (character.gameObject != origin.gameObject && character.gameObject.CompareTag(origin.gameObject.tag) && character.Healable())
                {
                    //Debug.Log("");
                    float temp = Vector3.Distance(origin.transform.position, character.transform.position);
                    if (temp < distanceTemp)
                    {
                        distanceTemp = temp;
                        result = character;
                    }
                    //Debug.Log(distanceTemp);
                }
            }
            if (result == origin)
                return null;
            return result;
        }
        else
        {
            return null;
        }
    }

    public Character GetClosestCharater(Character origin)
    {
        // At least have another to compare with
        if(characterList.Count > 1)
        {
            Character result = characterList[0];
            float distanceTemp = 999;
            foreach (Character character in characterList)
            {
                // I do not compare to myself
                if(character != origin)
                {
                    float temp = Vector3.Distance(origin.transform.position, character.transform.position);
                    if(temp < distanceTemp)
                    {
                        distanceTemp = temp;
                        result = character;
                    }
                }
            }
            return result;
        }
        else
        {
            return null;
        }
    }

    public Character GetClosestEnemy(Character origin)
    {
        // At least have another to compare with
        if (characterList.Count > 1)
        {
            Character result = characterList[0];
            float distanceTemp = 999;
            foreach (Character character in characterList)
            {
                // I do mpt compare to myself AND I look for different tag
                if (character != origin && !character.gameObject.CompareTag(origin.gameObject.tag))
                {
                    
                    float temp = Vector3.Distance(origin.transform.position, character.transform.position);
                    if (temp < distanceTemp)
                    {
                        distanceTemp = temp;
                        result = character;
                    }
                    //Debug.Log(distanceTemp);
                }
            }
            Debug.Log(origin.name + " fighting " +result.name);
            return result;
        }
        else
        {
            return null;
        }
    }

    //检测最近的敌人是否进入character攻击范围
    public bool IfEntityInAttackRange(Character origin)
    {
        if (origin==null)
        {
            return false;
        }
        Character enemy = GetClosestEnemy(origin);
        return (origin.AttackRange >= Vector3.Distance(enemy.transform.position, origin.transform.position));
        
    }
    //检测最近的友方是否进入character恢复范围
    public bool IfEntityInHealRange(Character origin)
    {
        if (origin == null)
        {
            return false;
        }
        Character ally = GetClosestHealableAlly(origin);
        if (ally == null)
        {
            return false;
        }
        return (origin.AttackRange >= Vector3.Distance(ally.transform.position, origin.transform.position));

    }

    public void RemoveCharacter(Character character)
    {
        characterList.Remove(character);
    }
}
