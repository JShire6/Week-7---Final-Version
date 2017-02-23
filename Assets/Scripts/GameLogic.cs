using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    Player playerComponent;
    Enemy enemyComponent;
    GameObject enemyGameObject;

    public AudioSource[] sounds;
    public AudioSource noise1;
    public AudioSource noise2;
    public AudioSource noise3;
    bool gameOverSound;
    bool winStateSound;

    public bool gameOver = false; // is the game over?

    // Use this for initialization
    void Start()
    {
        // find the player component
        playerComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        //Sounds file Assets
        sounds = GetComponents<AudioSource>();
        
        gameOverSound = false;
        winStateSound = false;        

        //plays level sound
        noise1.Play();

    }

    // Update is called once per frame
    void Update()
    {
        // if the player number of lives is zero, game over

        if (playerComponent.Lives == 0)
        {
            gameOver = true;

            //Play Game Over sound
            if (!gameOverSound)
            {
                noise1.Stop();
                noise2.Play();

                gameOverSound = true;
            }
            // pause the game
            Time.timeScale = 0.0f;

            CharacterController controller = GetComponent<CharacterController>();

            if(Input.GetButton ("Jump"))
            {
                SceneManager.LoadScene("mario");

                enemyComponent.Reset();

                Time.timeScale = 1.0f;
            }
        }

        if (playerComponent.winState == true && winStateSound == false)
        {
            noise1.Stop();
            noise3.Play();

            winStateSound = true;
        }
    }
}
