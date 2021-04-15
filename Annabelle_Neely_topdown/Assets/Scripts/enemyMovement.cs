using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float detectionRadius = 3;
    public float movementSpeed = 6;
    public bool canMove = false;
    public bool movementDirection = false;
    public bool isFollowing = false;

    public Transform playerTarget;
    private CircleCollider2D detectionZone;
    private Rigidbody2D myRB;
    private Vector2 up;
    private Vector2 down;
    private Vector2 zero;

    // Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementSpeed);
        down = new Vector2(0, -movementSpeed);
        zero = new Vector2(0, 0);

        playerTarget = GameObject.Find("Player X").transform;

        myRB = GetComponent<Rigidbody2D>();
        detectionZone = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionZone.radius = detectionRadius;

        if (isFollowing == false)
            myRB.velocity = zero;

        else if (isFollowing == true)
        {
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            myRB.MovePosition(transform.position + (lookPos * movementSpeed * Time.deltaTime));
        }

        if (canMove == true)
        {
            if (movementDirection == false)
                myRB.velocity = down;

            else if (movementDirection == true)
                myRB.velocity = up;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bullet"))
        {
            Destroy(collision.gameObject);

            this.gameObject.SetActive(false);

            GameObject.Find("GameManager").GetComponent<GameManager>().playerKillCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
            isFollowing = true;

       // if (collision.gameObject.name == "trigger1")
         //   movementDirection = false;

     //  else if (collision.gameObject.name == "trigger1")
         //   movementDirection = true;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player X"))
            isFollowing = false;
    }
}
