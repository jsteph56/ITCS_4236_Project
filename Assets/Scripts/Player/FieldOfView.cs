using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(1, 360)] public float angle = 45f;
    public float radius = 5f;
    public LayerMask enemyLayer;
    public LayerMask obstructionLayer;

    private GameObject player;
    private GameObject[] enemies;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }
    
    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
    
        if (rangeCheck.Length > 0)
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                GameObject enemy = rangeCheck[i].gameObject;
                Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            
                if (Vector2.Angle(transform.up, directionToEnemy) < angle / 2)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                    if (!Physics2D.Raycast(transform.position, directionToEnemy, distanceToEnemy, obstructionLayer))
                        enemy.GetComponent<EnemyController>().visibleByPlayer = true;
                    else
                        enemy.GetComponent<EnemyController>().visibleByPlayer = false;
                }
                else
                {
                    enemy.GetComponent<EnemyController>().visibleByPlayer = false;
                }
            }
        }
        else
        {
            // Debug.Log(enemies.Length);
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<EnemyController>().visibleByPlayer)
                {
                    enemy.GetComponent<EnemyController>().visibleByPlayer = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    
        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);
   
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        foreach(GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyController>().visibleByPlayer)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
