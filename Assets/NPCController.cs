using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    bool playerInRange = false;
    NPCMessages npcM;
    MessageManager messageManager;
    public Transform ship;
    public Transform destination;
    bool travel= false;
    // Start is called before the first frame update
    void Start()
    {
        npcM = GetComponent<NPCMessages>();
        messageManager = GameObject.Find("MessageManager").GetComponent<MessageManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            messageManager.StartMessage(npcM.messages[0]);
            travel = true;
        }
        if (travel)
        {
            print("traveling");
            ship.position =Vector3.MoveTowards(ship.position, destination.position, 0.05f);
            if (Vector3.Distance(ship.position, destination.position) < 0.1)
            {
                travel = false;
            }
        }

    }
}
