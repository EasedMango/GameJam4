using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public PlayerController player;
    public GameObject[] buildings;
    private int currentBuilding = 0;
    bool building = false;
    bool playerInRange = false;
    public Vector3 placement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentBuilding = player.currentBuilding;
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            building = !building;
            player.UpdateMode(1);
            Destroy(GameObject.FindWithTag("BuildModeObject"));
            print("inBuild");
        }
        if (building)
        {
            player.UpdateMode(2);
            PlaceBuilding();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }


    void PlaceBuilding()
    {

        if (!GameObject.FindWithTag("BuildModeObject"))
        {
            //   print(buildings[currentBuilding].name);
            Instantiate(buildings[currentBuilding]).name = buildings[currentBuilding].name;
            GameObject.FindWithTag("BuildModeObject").transform.position = placement;
        }
        else if (buildings[currentBuilding].name != GameObject.FindWithTag("BuildModeObject").name)
        {
            Destroy(GameObject.FindWithTag("BuildModeObject"));
           
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.material > 0)
        {
            building = !building;
            player.UpdateMode(1);
            player.currentBuilding -=1;
            GameObject.FindWithTag("BuildModeObject").tag = "Habitat";
            Destroy(gameObject);
        }

    }


}
    
