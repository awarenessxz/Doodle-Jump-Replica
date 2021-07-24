using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]     // ensures rigidbody2d always exists
public class Player : MonoBehaviour
{
    public Text scoreText;
    public Text startText;
    public float movementSpeed = 10f;
    float moveInput = 0f;
    Rigidbody2D rb;
    private float topScore = 0f;
    private bool isStarted;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStarted == false)
        {
            isStarted = true;
            startText.gameObject.SetActive(false);
            rb.gravityScale = 1;
        }
        else
        {
            moveInput = Input.GetAxis("Horizontal");
            UpdateSprite();
        }
        UpdateScoreText();
    }

    // All input movement should be done here
    void FixedUpdate()
    {
        if (isStarted)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = moveInput * movementSpeed;
            rb.velocity = velocity;
        }
    }

    private void UpdateScoreText()
    {
        if (rb.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }
        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }

    private void UpdateSprite()
    {
        if (moveInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
