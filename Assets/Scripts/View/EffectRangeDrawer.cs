using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectRangeDrawer : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlane;
    public float circleRadius;
    public int segments = 64;
    private Mesh circleMesh;
    private Material rangeMaterial;
    private Vector3 mouseWorldPosition;
    private List<Character> targets;
    private void Awake()
    {
        targets = new List<Character>();
        targetPlane = GameObject.Find("Plane").transform;
        rangeMaterial = Resources.Load<Material>("Material/RangeIndicator");
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeVisual();
    }

    public void InitializeVisual()
    {
        
        circleMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = circleMesh;
        GetComponent<Renderer>().material = rangeMaterial;
        //transform.parent = targetPlane;
        transform.eulerAngles = new Vector3(180, 0, 0);
        transform.position = targetPlane.position + Vector3.up * 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawRange();
    }

    void DrawRange()
    {
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            float x = Mathf.Sin(angle) * circleRadius;
            float z = Mathf.Cos(angle) * circleRadius;
            vertices[i] = new Vector3(x, 0f, z);
        }

        for (int i = 0, j = 1; i < segments; i++, j++)
        {
            if (j == segments)
                j = 0;

            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = j + 1;
            triangles[i * 3 + 2] = j;
        }
        circleMesh.Clear();
        circleMesh.vertices = vertices;
        circleMesh.triangles = triangles;
        circleMesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = circleMesh;


        // 获取鼠标在世界空间中的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 如果射线与平面相交，则获取交点的位置
            if (hit.transform == targetPlane)
            {
                mouseWorldPosition = hit.point;
                transform.position = new Vector3(mouseWorldPosition.x, (targetPlane.position + Vector3.up * 0.01f).y, mouseWorldPosition.z);
            }
        }
    }

    public List<Character> GetCollideCharacters ()
    {
        HighLightAllTargets(0);
        targets.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, circleRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.GetComponent<Character>())
            {
                // 处理碰撞的对象
                targets.Add(collider.gameObject.GetComponent<Character>());
            }
        }
        HighLightAllTargets(1);
        return targets;
    }

    public List<Character> GetHoverCharacter()
    {
        HighLightAllTargets(0);
        targets.Clear();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 获取与射线相交的游戏对象
            GameObject hoveredObject = hit.collider.gameObject;

            // 如果新的对象不为空，并且不是UI对象
            if (hoveredObject != null && !hoveredObject.GetComponent<UIBehaviour>() && hoveredObject.GetComponent<Character>())
            {
                targets.Add(hoveredObject.GetComponent<Character>());
            }
        }
        HighLightAllTargets(1);
        return targets;
    }

    private void HighLightAllTargets(int v)
    {
        foreach (Character character in targets)
        {
            character.HighLightMe(v);
        }
    }

    private void OnDestroy()
    {
        HighLightAllTargets(0);
    }

}
