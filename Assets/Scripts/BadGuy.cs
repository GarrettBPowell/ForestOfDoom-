using UnityEngine;
using UnityEngine.SceneManagement;

public class BadGuy : MonoBehaviour
{

    public bool goForward = false;

    public float moveSpeed = 0.2f;
    Animator animator;

    SpriteRenderer spriteRenderer;
    GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // move back and forth between point A and B
        // move forward
        if (goForward)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        } 
        else // go backward
        {
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D otherGuy) 
    {
        if (otherGuy.gameObject.tag == "Player") 
        {
            gameManager.maxLivesLeft--;
            if (gameManager.maxLivesLeft < 0)
            {
                //load game over scene instead
                Debug.Log("pain");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "pointA") 
        {
            goForward = false;
        }
        else if (other.gameObject.tag == "pointB")
        {
            goForward = true;
        }
    }
}
