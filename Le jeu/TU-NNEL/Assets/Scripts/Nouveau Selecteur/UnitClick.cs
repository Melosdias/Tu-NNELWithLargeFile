using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask unités;


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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unités))
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
            else                                      //cliqué sur R
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
            }
        }
        
    }
}
