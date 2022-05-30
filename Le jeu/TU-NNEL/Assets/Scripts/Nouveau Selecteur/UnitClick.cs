using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask unit�s;


    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unit�s))
            { 
                if (Input.GetKey(KeyCode.LeftShift))  //shiftclick
                {
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else                                  //click normal
                {
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else                                      //cliqu� sur R
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
            }
        }
        
    }
}
