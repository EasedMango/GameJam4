using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatureController : MonoBehaviour
{
    //Dialogue TriggerStates
    private bool firstMessage = false;
    private bool playerInRange = false;
    private NPCMessages messages;
    public UiManager uiManager;
    public GameObject[] patrolPoints;
    public Transform foodBowl;
    public Transform drinkBowl;
    public Transform bed;

    public GameObject winScreen;

    public SpriteRenderer h;
    public SpriteRenderer s;
    public SpriteRenderer t;

    private Transform destination;
    public float speed = 1;
    public float hunger = 75;
    public float thirst = 75;
    public float sleep = 50;
    bool gettingNecessary = false;
    bool traveling = false;
    private float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        destination = patrolPoints[Random.Range(0, patrolPoints.Length)].transform;
        moveTimer = Random.Range(3, 8);
        messages = GetComponent<NPCMessages>();
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        if (GameObject.FindGameObjectWithTag("Habitat"))
        {
            //transform.position = GameObject.FindGameObjectWithTag("Habitat").transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {




        if (GameObject.FindGameObjectWithTag("Habitat"))
        {

            h.color = new Color(hunger, 100 - hunger, 0);
            s.color = new Color(sleep, 100 - sleep, 0);
            t.color = new Color(thirst, 100 - thirst, 0);
            hunger = Mathf.Clamp(hunger + Time.deltaTime * 0.1f, 0, 100);
            thirst = Mathf.Clamp(thirst + Time.deltaTime * 0.1f, 0, 100);
            sleep = Mathf.Clamp(sleep + Time.deltaTime * 0.1f, 0, 100);


            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                
                if (Vector2.Distance(transform.position, destination.position) >= 0.1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destination.position, 0.001f);

                }
            }
            if (Vector2.Distance(transform.position, destination.position) <= 0.1)
            {

                destination = patrolPoints[Random.Range(0, patrolPoints.Length)].transform;
                moveTimer = Random.Range(3, 8);
                if (gettingNecessary)
                    gettingNecessary = false;
                if (!gettingNecessary)
                {
                    int ran = Random.Range(0, 300);
                    print("First Check: " + ran + " < " + (sleep + hunger + thirst));
                    if (ran < (sleep + hunger + thirst))
                    {
                        ran = Random.Range(0, 100);
                        print(ran + " < " + (sleep));
                        if (ran < sleep)
                        {
                            print("sleep");
                            gettingNecessary = true;
                            destination = bed;
                        }
                        ran = Random.Range(0, 100);
                        print(ran + " < " + (hunger));
                        if (ran < hunger)
                        {
                            print("hunger");
                            gettingNecessary = true;
                            destination = foodBowl;
                        }
                        ran = Random.Range(0, 100);
                        print(ran + " < " + (thirst));
                        if (ran < thirst)
                        {
                            print("thirst");
                            gettingNecessary = true;
                            destination = drinkBowl;
                        }
                    }
                }
            }




        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(name == "Cre1")
            {
                GameObject.FindWithTag("Win").GetComponent<RectTransform>().localPosition = Vector3.zero;
                GameObject.Find("Player").GetComponent<PlayerController>().UpdateMode(0);
            }
            if (!firstMessage)
            {
                print("messageOne");
                TriggerDialogue(messages.messages[0]);
                firstMessage = true;
                uiManager.UpdateObjectiveText("Refill the food and drink\nPress: E on the bowls");
            }
            else
            {
                print("messageTwo");
                TriggerDialogue(messages.messages[1]);
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

    public void TriggerDialogue(Message message)
    {
        FindObjectOfType<MessageManager>().StartMessage(message);

    }


}
