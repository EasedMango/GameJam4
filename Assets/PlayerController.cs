using UnityEngine;
using UnityEditor;


public class PlayerController : MonoBehaviour
{
    //



    public enum Modes { StillMode = 0, MoveMode, BuildMode }
    public float speed = 2;
    public Modes mode = Modes.MoveMode;
    public Modes prevMode;

    public int currentBuilding = 0;
    Rigidbody2D rb2d;
    private bool BuildModeActive = false;

    public GameObject[] buildings;
    ContactFilter2D filter2D;
    public LayerMask mask;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        prevMode = Modes.MoveMode;
        filter2D = new ContactFilter2D
        {
            useLayerMask = true,
            useTriggers = false
        };
        filter2D.SetLayerMask(LayerMask.NameToLayer("Ship"));
    }

    // Update is called once per frame
    void Update()
    {

        switch (mode)
        {
            case Modes.StillMode:
                StillMode();
                break;
            case Modes.MoveMode:
                MoveMode();
                break;
            case Modes.BuildMode:
                BuildMode();
                break;
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (mode == Modes.BuildMode)
                UpdateMode(1);
            else
                UpdateMode(2);

        }


    }

    public void UpdateMode(int mode_)
    {
        Destroy(GameObject.FindWithTag("BuildModeObject"));
        mode = (Modes)mode_;
    }

    private void BuildMode()
    {
        Camera.main.orthographicSize = 10;
        rb2d.velocity = Vector3.zero;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool overUI = GameObject.Find("UiManager").GetComponent<UiManager>().IsPointerOverUIElement();

        PlaceBuilding(mouse);

        if (!GameObject.FindWithTag("BuildModeObject"))
        {
            //   print(buildings[currentBuilding].name);
            Instantiate(buildings[currentBuilding]).name = buildings[currentBuilding].name;
        }

        if (buildings[currentBuilding].name + "(Clone)" != GameObject.FindWithTag("BuildModeObject").name)
        {
            Destroy(GameObject.FindWithTag("BuildModeObject"));
            Instantiate(buildings[currentBuilding]);
        }
        else if (!overUI && Input.GetMouseButtonDown(0))
        {
            GameObject.FindWithTag("BuildModeObject").tag = "Untagged";
            //PlaceBuilding(building)
        }
        else if (!overUI)
        {
            GameObject.FindWithTag("BuildModeObject").transform.position = new Vector2(Mathf.Round(mouse.x), Mathf.Round(mouse.y));
        }


    }



    private void PlaceBuilding(Vector2 origin)
    {
        Ray2D ray = new Ray2D(origin, Vector2.right);
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int totalObjectsHit = Physics2D.Raycast(origin,Vector2.right, filter2D, hits);
       

        Collider2D[] inRange =  Physics2D.OverlapCircleAll(new Vector2(Mathf.Round(origin.x) + 0, Mathf.Round(origin.y) + 4),6.5f,mask);

        Debug.DrawRay(new Vector2(Mathf.Round(origin.x) + 0, Mathf.Round(origin.y) + 4), Vector3.right * 6.5f);
        for (int i = 0; i < inRange.Length; i++)
        {
          
            
        }



            Debug.DrawRay(origin, Vector2.right, Color.red);

    }

    private void MoveMode()
    {



        Camera.main.orthographicSize = 5;
        //  print("MoveMode");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector3(horizontal, vertical, 0f).normalized * speed;
    }

    private void StillMode()
    {
        rb2d.velocity = Vector3.zero;
    }






}
