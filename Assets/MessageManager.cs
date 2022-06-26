using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    public PlayerController playerController;
    public GameObject bg;

    private Queue<string> messageQueue = new Queue<string>();
    private Queue<string> nameQueue = new Queue<string>();


    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new Queue<string>();
        nameQueue = new Queue<string>();
        bg.SetActive( false);
    }

    public void StartMessage(Message message)
    {
       nameText.text = message.names[0];
        playerController.mode = PlayerController.Modes.StillMode;
        messageQueue.Clear();
        nameQueue.Clear();  
        foreach (string sentence in message.messages)
        {
            messageQueue.Enqueue(sentence);
        }
        foreach(string name in message.names)
        {
            nameQueue.Enqueue(name);
        }

        DisplayNextSentence();
        bg.SetActive(true);
    }

    public void DisplayNextSentence()
    {
        if(messageQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string message = messageQueue.Dequeue();
        string name = nameQueue.Dequeue();
        messageText.text=(message);
        nameText.text= name;
    }

    private void EndDialogue()
    {
        bg.SetActive(false);
        playerController.mode = playerController.prevMode;
       print("End of Message");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
