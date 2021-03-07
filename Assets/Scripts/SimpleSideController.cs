using UnityEngine;
using UnityEngine.UI;

public class SimpleSideController : MonoBehaviour
{

    public float moveSpeed = 10.0f;

    public float jumpForce = 300.0f;

    public bool isGrounded = false;

    public float bulletForce = 50.0f;

    Rigidbody2D blahblah;

    Animator animator;

    SpriteRenderer spriteRenderer;

    public GameObject spawnPoint;
    public GameObject energyBall;
    public bool fireForward = true;

    public Joystick joystick;

    public Button jump;
    public bool buttonClicked;

    // Start is called before the first frame update
    void Start()
    {
        blahblah = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Button btn = jump.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        buttonClicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        // What Moves Us
        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalJoyInput = joystick.Horizontal;
        //Get the value of the Horizontal input axis.

        if(horizontalInput != 0)
            transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
        else if(horizontalJoyInput != 0)
            transform.Translate(new Vector3(horizontalJoyInput, 0, 0) * moveSpeed * Time.deltaTime);


        if (horizontalInput > 0 || horizontalJoyInput > 0) 
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
            fireForward = true;
        } 
        else if (horizontalInput < 0 || horizontalJoyInput < 0) 
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
            fireForward = false;
        } 
        else 
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate() 
    {
        if ((Input.GetButton("Jump") || buttonClicked) && isGrounded) 
        {
            blahblah.AddForce(transform.up * jumpForce);
            animator.SetBool("isJumping", true);
            buttonClicked = false;
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetTrigger("isAttack");
            // now instantiate the ball and propel forward
            FireEnergyBall();
        }
    }

    void FireEnergyBall() 
    {
        // the Bullet instantiation happens here
        GameObject brandNewPewPew;
        brandNewPewPew = Instantiate(energyBall, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
 
        // get the Rigidbody2D component from the instantiated Bullet and control it
        Rigidbody2D tempRigidBody;
        tempRigidBody = brandNewPewPew.GetComponent<Rigidbody2D>();
 
        // tell the bullet to be "pushed" by an amount set by bulletForce 
        if (fireForward == true) {
            // fireForward is fire to the right
            tempRigidBody.AddForce(transform.right * bulletForce);
        } else {
            // fire left, a.k.a., "negative-right"
            tempRigidBody.AddForce(-transform.right * bulletForce);
        }
        
 
        // basic Clean Up, set the Bullets to self destruct after 5 Seconds
        Destroy(brandNewPewPew, 5.0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Jumpy") 
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Jumpy") 
        {
            isGrounded = false;
        }
    }

}
