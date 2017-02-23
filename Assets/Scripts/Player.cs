using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // variables taken from CharacterController.Move example script
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    int direction;

    public int health = 1;
    public bool alive = true;
    bool fire_power;
    public string fire_button;
    public GameObject fire_ball;

    public Material base_colour;
    public Material fire_colour;

    Vector3 small_size;
    Vector3 big_size;

    public float deathTimerMax;
    float deathTimer;

    public int Score = 1; //score of player
    public GameObject coin;

    GameObject enemyGameObject;

    Enemy enemyComponent;

    private AudioSource jumpSound;


    public int Lives = 3; // number of lives the player hs

    public bool winState = false;


    Vector3 start_position; // start position of the player


    void Start()
    {

        deathTimer = deathTimerMax;
        fire_power = false;
        direction = 0;

        small_size = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        big_size = new Vector3(this.transform.localScale.x, (this.transform.localScale.y) * 1.5f, this.transform.localScale.z);

        // record the start position of the player
        start_position = transform.position;
        jumpSound = GetComponent<AudioSource>();
        enemyGameObject = GameObject.FindGameObjectWithTag("Enemy");
        enemyComponent = enemyGameObject.GetComponent<Enemy>();

    }

    public void Reset()
    {
        // reset the player position to the start position
        transform.position = start_position;
    }

    public void ChangeSize()
    {
        alive = false;
        fire_power = false;
        health--;
        if (health == 2)
        {
            this.GetComponent<MeshRenderer>().material = base_colour;
        }
        else if (health == 1)
        {
            this.transform.localScale = small_size;
        }
    }

    void Update()
    {
        Physics.IgnoreLayerCollision(8, 11);

        // get the character controller attached to the player game object
        CharacterController controller = GetComponent<CharacterController>();


        // check to see if the player is on the ground
        if (controller.isGrounded)
        {
            // set the movement direction based on user input and the desired speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            if (Input.GetAxis("Horizontal") > 0)
            {
                direction = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                direction = -1;
            }

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // check to see if the player should jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                jumpSound.Play();
            }

        }
        else if (transform.position.y < -4)
        {
            Lives = Lives - 1;


            
            //enemyComponent.Reset();
           

            Reset();
        }

        // apply gravity to movement direction
        moveDirection.y -= gravity * Time.deltaTime;

        // make the call to move the character controller
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown(fire_button) && fire_power == true)
        {
            GameObject f_ball = Instantiate(fire_ball, new Vector3(this.transform.position.x + direction, this.transform.position.y), Quaternion.identity) as GameObject;
            f_ball.SendMessage("SetDirection", direction);
        }


        if (alive == false)
        {
            deathTimer -= 1 * Time.deltaTime;

            Physics.IgnoreLayerCollision(8, 9);
            if (deathTimer < 0)
            {
                deathTimer = deathTimerMax;
                alive = true;
                Physics.IgnoreLayerCollision(8, 9, false);
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Flag"))
        {
            winState = true;

            Time.timeScale = 0.0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "greenMushroom")
        {
            Lives++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "redMushroom")
        {
            if (health != 3)
            {
                health = 2;
            }
            this.transform.localScale = big_size;
            Destroy(other.gameObject);
        }
        else if (other.tag == "fireFlower")
        {
            //fire powers
            health = 3;
            fire_power = true;
            this.transform.localScale = big_size;
            this.GetComponent<MeshRenderer>().material = fire_colour;
            Destroy(other.gameObject);
        }

        if (other.tag == "qMarkBox")
        {
            other.SendMessage("SpawnPickUp");
        }
    }


}