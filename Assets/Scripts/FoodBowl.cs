using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBowl : MonoBehaviour
{

    public float foodAmount = 0;
    bool playerInRange = false;
    public bool firstCheck = false;
    public Message check;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.name == "Creature")
        {

            CreatureController creatureController = collision.GetComponent<CreatureController>();

            if (foodAmount > 0 && creatureController.hunger != 0)
            {
                float hunger = creatureController.hunger;
                float hungerAmount_ = foodAmount;


                creatureController.hunger = Mathf.Clamp(hunger - hungerAmount_, 0, 100);
                foodAmount = Mathf.Clamp(hungerAmount_ - hunger, 0, 100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerInRange = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!firstCheck && GameObject.Find("Drink").GetComponent<DrinkBowl>().firstCheck)
            {
                GameObject.Find("MessageManager").GetComponent<MessageManager>().StartMessage(check);
                firstCheck = true;
            }
            else
            {
                firstCheck = true;
            }
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
          
            if (player.food >= 1)
            {
                foodAmount = 100;
                player.food -= 1;
            }
            else
            {
                GameObject.Find("UiManager").GetComponent<UiManager>().SendAlert("You don't have enough food");
            }
        }
    }
}
