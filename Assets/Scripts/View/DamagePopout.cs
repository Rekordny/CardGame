using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopout : MonoBehaviour
{
    public GameObject damageTextPrefab; // 伤害文本预制体
    public float popoutDuration = 1f; // 伤害弹出持续时间
    public float popoutSpeed = 1f; // 伤害弹出速度

    public void ShowDamagePopout(float damageAmount, Character character)
    {
        // 动态生成文本对象
        GameObject popoutObject = Instantiate(damageTextPrefab, character.transform.position, Quaternion.identity, character.gameObject.transform.Find("Visual/HealthBar"));
        popoutObject.transform.position += Vector3.up;
        popoutObject.transform.position += Vector3.right;
        popoutObject.transform.rotation *= Quaternion.Euler(45f, 0f, 0f);
        TextMeshProUGUI popoutText = popoutObject.GetComponent<TextMeshProUGUI>();
        
        if (damageAmount >= 0)
        {
            popoutText.color = Color.red;
            popoutText.text = damageAmount.ToString();
        }
        else
        {
            popoutText.color = Color.green;
            popoutText.text = Mathf.Abs(damageAmount).ToString();
        }
        // 启动协程处理伤害弹出效果
        StartCoroutine(AnimatePopout(popoutObject));
    }

    private IEnumerator AnimatePopout(GameObject popoutObject)
    {
        float elapsedTime = 0f;

        while (elapsedTime < popoutDuration)
        {
            // 上升效果
            popoutObject.transform.Translate(Vector3.up * popoutSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 销毁文本对象
        Destroy(popoutObject);
    }

}
