using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTrigger : MonoBehaviour
{
    public bool destroyOnClick = false;
    public Message message;
  
    public void TriggerDialogue()
    {
        FindObjectOfType<MessageManager>().StartMessage(message);
        if (destroyOnClick) {

            GameObject.Find("StartScreen").gameObject.SetActive(false);

                
                };
    }
}
