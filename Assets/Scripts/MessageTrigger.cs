using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public bool destroyOnClick = false;
    public Message message;
  
    public void TriggerDialogue()
    {
        FindObjectOfType<MessageManager>().StartMessage(message);
        if (destroyOnClick) Destroy(gameObject);
    }
}
