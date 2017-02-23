using UnityEngine;
using System.Collections;

public class coinbehaviour : MonoBehaviour
{
    GameObject playerGameObject; // this is a reference to the player game object
    // Use this for initialization
    public int coinValue = 1;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.name)
        {
            case "Player":
                Destroy(this.gameObject);
                Player playerComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                playerComponent.Score = playerComponent.Score + coinValue;
                audioSource.Play();

                break;

        }
    }

    

}