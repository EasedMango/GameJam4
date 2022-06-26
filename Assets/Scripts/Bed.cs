using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.name == "Creature")
        {

            CreatureController creatureController = collision.GetComponent<CreatureController>();

            if (creatureController.sleep != 0)
            {

                creatureController.sleep = Mathf.Clamp(creatureController.sleep -(Time.deltaTime*1000), 0, 100);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
