using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform trans;
    [SerializeField] float moveSpeed;
    [SerializeField] float fadeSpeed;

    private GameObject player;
    private Vector3 playerPosition, startingPosition;
    private bool fadeOut, fadeIn;
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
        Fade();
    }

    void FixedUpdate()
    {
        //bool inPlayerRadius = distanceFromPlayer < player.GetComponent<FieldOfView>().radius + 55;
        
        if (!visibleByPlayer && !fadeOut && !fadeIn)
        {
            GetComponent<AIPath>().canMove = true;
        }
        else
        {
            GetComponent<AIPath>().canMove = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collision");
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerController>().fear += 25;
            FadeOutObject();
        }
    }

    public void Fade()
    {
        if (fadeOut)
        {
            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;
        
            if (objectColor.a <= 0)
            {
                transform.position = startingPosition;
                fadeOut = false;
                FadeInObject();
            }
        }

        if (fadeIn)
        {
            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;

            if (objectColor.a >= 1)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeOutObject()
    {
        fadeOut = true;
    }

    public void FadeInObject()
    {
        fadeIn = true;
    }
}
