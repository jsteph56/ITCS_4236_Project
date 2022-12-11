using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Pathfinding;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite stoneSprite;
    [SerializeField] FOV fieldOfView;

    private GameObject[] enemies;
    private float fadeSpeed;
    private bool fadeIn, fadeOut, isStone;

    public float stoneTouched;
    public bool hasKey;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        fadeSpeed = .3f;
        fadeIn = false;
        fadeOut = false;
        isStone = false;

        hasKey = false;
        stoneTouched = 0f;

        Cursor.visible = false;
    }

    void Update()
    {
        fieldOfView.SetAimDirection(GetMousePosition());
        fieldOfView.SetOrigin(transform.position);

        if (stoneTouched >= 100)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<AIPath>().enabled = false;
            }

            if (!isStone)
            {
                FadeOutObject();
                isStone = true;
            }
        }

        Fade();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void Fade()
    {
        if (fadeOut)
        {
            Debug.Log("Entered fadeOut");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;

            if (objectColor.a <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = stoneSprite;

                fadeOut = false;
                FadeInObject();
            }
        }

        if (fadeIn)
        {
            Debug.Log("Entered fadeIn");
            Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;

            if (objectColor.a >= 1)
            {
                fadeIn = false;
                StartCoroutine(EndGame());
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

    IEnumerator EndGame()
    {
        SceneManager.LoadScene("Lose Scene");
        yield return null;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mouseDirection = Input.mousePosition;
        mouseDirection.z = 0f;
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
        return mouseDirection -= transform.position;
    }
}
