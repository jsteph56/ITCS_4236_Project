using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform trans;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 startingPosition;
    private float distanceFromPlayer;
    public bool visibleByPlayer = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        startingPosition = transform.position;
    }

    void Update()
    {
        playerPosition = player.transform.position;

        distanceFromPlayer = Vector2.Distance(trans.position, playerPosition);

        if (!this.enabled)
        {
            rb.MovePosition(startingPosition);
        }
    }

    void FixedUpdate()
    {
        //bool inPlayerRadius = distanceFromPlayer < player.GetComponent<FieldOfView>().radius + 55;
        
        if (!visibleByPlayer)
        {
            //Vector2 directionToTarget = (playerPosition - trans.position).normalized;
            trans.position = Vector2.MoveTowards(trans.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collision");
        if (collision.gameObject != player) return;

        player.GetComponent<PlayerController>().fear += 25;
        this.enabled = false;
        Debug.Log(this.enabled);
        GetComponent<FadeEnemy>().FadeOutObject();
    }
}
