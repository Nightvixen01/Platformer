using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2 : MonoBehaviour
{
    private Rigidbody2D myRB;
    public GameObject bullet;
    public AudioClip laser;
    public AudioClip Pickup;
    public AudioClip Hit;
    public float speed = 7;
    public int playerHealth = 3;
    public float bulletSpeed = 16;
    public float bulletLifespan = 1;
    private AudioSource speaker;
   

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        speaker = GetComponent<AudioSource>();
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }

        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

            speaker.clip = laser;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);

            speaker.clip = laser;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

            speaker.clip = laser;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

            speaker.clip = laser;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("enemy"))
        {
            playerHealth--;

            speaker.clip = Hit;
            speaker.Play();

        }

        else if (collision.gameObject.name.Contains("pickup") && playerHealth < 3)
        {
            playerHealth++;

            speaker.clip = Pickup;
            speaker.Play();

            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "enemyTrigger")
        {
            GameObject.Find("enemy").GetComponent<enemyMovement>().canMove = true;
            GameObject.Find("enemy").GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            Destroy(collision.gameObject);
        }
    }

}

