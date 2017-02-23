using UnityEngine;
using System.Collections;

public class CoinCube : MonoBehaviour {

    // Use this for initialization
    public GameObject coin;
    Player PlayerComponent;

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.name)
        {
            case "Player":
                Instantiate(coin, GameObject.FindGameObjectWithTag("CoinCube").transform.position + (transform.up * 2), GameObject.FindGameObjectWithTag("Coin").transform.rotation);
                Destroy(this.gameObject);
                //PlayerComponent.moveDirection = transform.TransformDirection(PlayerComponent.moveDirection);
                break;

        }
    }


    }

