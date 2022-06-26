using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    int UILayer;
    public GameObject buildMenu;
    public EventManager eventManager;
    public MessageManager messageManager;
    public PlayerController playerController;
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

    // Update is called once per frame
    void Update()
    {
        print(IsPointerOverUIElement() ? "Over UI" : "Not over UI");
        switch (playerController.mode)
        {
            case PlayerController.Modes.StillMode:
                objectiveText.gameObject.SetActive(false);
                break;
            case PlayerController.Modes.MoveMode:
                objectiveText.gameObject.SetActive(true);
                break;
            case PlayerController.Modes.BuildMode:
                Camera.main.orthographicSize = 10;
                objectiveText.gameObject.SetActive(true);
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
        Debug.Log("The cursor entered the selectable UI element.");
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
