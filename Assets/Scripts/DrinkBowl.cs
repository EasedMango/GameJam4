using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBowl : MonoBehaviour
{
    public float drinkAmount = 0;
    bool playerInRange = false;
    public bool firstCheck= false;
    public Message check;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.name == "Creature" && collision.TryGetComponent(typeof(CircleCollider2D),out Component c))
        {

            CreatureController creatureController = collision.GetComponent<CreatureController>();

            if(drinkAmount > 0 && creatureController.thirst !=0)
            {
                float thirst = creatureController.thirst;
                float drinkAmount_ = drinkAmount;


                creatureController.thirst = Mathf.Clamp(thirst - drinkAmount_, 0, 100);
                drinkAmount = Mathf.Clamp(drinkAmount_ - thirst, 0, 100);
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
            if (!firstCheck && GameObject.Find("Food").GetComponent<FoodBowl>().firstCheck)
            {
                GameObject.Find("MessageManager").GetComponent<MessageManager>().StartMessage(check);
                firstCheck = true;
            }
            else
            {
                firstCheck = true;
            }
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
           
            if (player.drink >= 1)
            {

                drinkAmount = 100;
                player.drink -= 1;
            }
            else
            {
                GameObject.Find("UiManager").GetComponent<UiManager>().SendAlert("You don't have enough for them to drink");
            }
        }
    }
}
