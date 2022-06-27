using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suplies : MonoBehaviour
{
    bool playerInRange=false;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            playerInRange=true;
            playerInRange=collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerInRange = true;
        }
        }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerController.food = 1;
            playerController.drink = 1;
            
        }
    }
}
