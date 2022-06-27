using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI alertText;
    int UILayer;
    public GameObject buildMenu;
    public EventManager eventManager;
    public MessageManager messageManager;
    public PlayerController playerController;

    bool alert;
    float alertTime;
    // Start is called before the first frame update
    void Start()
    {
        objectiveText.text = " ";
        UILayer = LayerMask.NameToLayer("UI");
        buildMenu.SetActive(false);
    }

    public void UpdateObjectiveText(string objetive)
    {
        objectiveText.text = objetive;

    }

    public void SendAlert(string alertMessage)
    {
        alert = true;
        print(alertMessage);
        alertTime = 2;
        alertText.text = alertMessage;

    }

    public void UpdateInventoryText()
    {
        inventoryText.text = "Inventory: "+
                            "\n Building Material: " + playerController.material +
                             "\n Food: " + playerController.food +
                             "\n Drink: " + playerController.drink;
    }

    // Update is called once per frame
    void Update()
    {
        if (alert)
        {
            if(alertTime <= 0) { alert = false; }
            alertTime -= Time.deltaTime;
            alertText.gameObject.SetActive(true);
        }
        else
        {
            alertText.gameObject.SetActive(false);
        }
        UpdateInventoryText();
        inventoryText.text = "Inventory: " +
                            "\n Building Material: " + playerController.material +
                             "\n Food: " + playerController.food +
                             "\n Drink: " + playerController.drink;
        // print(IsPointerOverUIElement() ? "Over UI" : "Not over UI");
        switch (playerController.mode)
        {
            case PlayerController.Modes.StillMode:
                objectiveText.gameObject.SetActive(false);
                inventoryText.gameObject.SetActive(false);
                buildMenu.SetActive(false);
                break;
            case PlayerController.Modes.MoveMode:
                objectiveText.gameObject.SetActive(true);
                inventoryText.gameObject.SetActive(true);
                buildMenu.SetActive(false);
                break;
            case PlayerController.Modes.BuildMode:
                Camera.main.orthographicSize = 10;
                objectiveText.gameObject.SetActive(true);
                inventoryText.gameObject.SetActive(true);
                buildMenu.SetActive(true);
                if (eventManager.firstBuild == false)
                {
                    messageManager.StartMessage(eventManager.firstBuildMessage);
                    eventManager.firstBuild=true;
                }
                switch (buildMenu.GetComponentInChildren<TextMeshProUGUI>().text)
                {
                    case "None":
                        playerController.currentBuilding = 0;
                        break;
                    case "Habitat":
                        playerController.currentBuilding = 1;
                        break;
                    case "Corridor":
                        playerController.currentBuilding = 2;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("The cursor entered the selectable UI element.");
    }




    




    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
