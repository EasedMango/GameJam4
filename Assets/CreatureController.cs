using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    //Dialogue TriggerStates
    private bool firstMessage = false;
    private bool playerInRange = false;
    private NPCMessages messages;
    public UiManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        messages = GetComponent<NPCMessages>();
        
    }

    // Update is called once per frame
    void Update()
    {

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                if (!firstMessage)
                {
                    print("messageOne");
                    TriggerDialogue(messages.messages[0]);
                    firstMessage = true;
                    uiManager.UpdateObjectiveText("Build Creature's Habitat\nPress: B");
                }
                else
                {
                    print("messageTwo");
                    TriggerDialogue(messages.messages[1]);
                }

            
        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        print("working");
        if (collision.name == "Player")
        {
            playerInRange = true;
        }
    }

    public void TriggerDialogue(Message message)
    {
        FindObjectOfType<MessageManager>().StartMessage(message);

    }


}
