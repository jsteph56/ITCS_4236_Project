using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

public class FOV : MonoBehaviour
{
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] LayerMask enemyMask;

    private GameObject[] enemies;
    private Vector3 origin;
    private Mesh mesh;
    private float fov;
    private float startingAngle;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        origin = Vector3.zero;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 40f;
    }

    void LateUpdate()
    {
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 3f;

        List<RaycastHit2D> enemyHits = new List<RaycastHit2D>();
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
    
            RaycastHit2D rayCast = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, obstructionMask);
            if (rayCast.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = rayCast.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            RaycastHit2D enemyRay = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, enemyMask);
            if (enemyRay.collider != null && enemyRay.collider.gameObject.tag == "Enemy")
            {
                enemyHits.Add(enemyRay);
            }

            vertexIndex++;
            angle -= angleIncrease;
        }
        
        if (enemyHits.Count > 0)
        {
            for(int i = 0; i < enemyHits.Count; i++)
            {
                enemyHits[i].collider.gameObject.GetComponent<EnemyController>().visibleByPlayer = true;
            }
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().visibleByPlayer = false;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0) n+= 360;
        return n;
    }
}
