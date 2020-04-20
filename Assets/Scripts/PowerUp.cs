using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        playerController = playerControllerObject.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        if (other.tag == "Player")
        {
            playerController.fireRate = playerController.fireRate * 0.5f;
            Destroy(gameObject);
        }
    }
}