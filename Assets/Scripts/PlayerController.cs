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

    public float food=1, drink=1, material=1;


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


    }

    public void UpdateMode(int mode_)
    {
        //Destroy(GameObject.FindWithTag("BuildModeObject"));
        mode = (Modes)mode_;
    }

    private void BuildMode()
    {
        Camera.main.orthographicSize = 10;
        rb2d.velocity = Vector3.zero;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool overUI = GameObject.Find("UiManager").GetComponent<UiManager>().IsPointerOverUIElement();






    }



    private void PlaceBuilding(Vector2 origin)
    {

       

        Collider2D[] inRange =  Physics2D.OverlapCircleAll(new Vector2(Mathf.Round(origin.x) + 0, Mathf.Round(origin.y) + 4),6.5f,mask);


        for (int i = 0; i < inRange.Length; i++)
        {
            print(inRange[i].name);
            
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
