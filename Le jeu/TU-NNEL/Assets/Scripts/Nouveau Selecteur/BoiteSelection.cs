using UnityEngine;

public class BoiteSelection : MonoBehaviour
{
    Camera myCam;

    [SerializeField]
    RectTransform Boite;

    Rect SelectionBox;

    Vector2 debut;
    Vector2 fin;

    void Start()
    {
        myCam = Camera.main;
        debut = Vector2.zero;
        fin = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            debut = Input.mousePosition;
            SelectionBox = new Rect();
        }

        if (Input.GetMouseButton(0))
        {
            fin = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnit();
            debut = Vector2.zero;
            fin = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 BoxStart = debut;
        Vector2 BoxEnd = fin;

        Vector2 BoxCenter = (BoxStart + BoxEnd)/ 2;
        Boite.position = BoxCenter;

        Vector2 Boxsize = new Vector2(Mathf.Abs(BoxStart.x - BoxEnd.x), Mathf.Abs(BoxStart.y - BoxEnd.y));
        Boite.sizeDelta = Boxsize;
    }

    void DrawSelection()
    {
        if(Input.mousePosition.x < debut.x)
        {
            SelectionBox.xMin = Input.mousePosition.x;
            SelectionBox.xMax = debut.x;
        }
        else
        {
            SelectionBox.xMin = debut.x;
            SelectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < debut.y)
        {
            SelectionBox.yMin = Input.mousePosition.y;
            SelectionBox.yMax = debut.y;
        }
        else
        {
            SelectionBox.yMin = debut.y;
            SelectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnit()
    {
        foreach (var unit in UnitSelection.Instance.unités)
        {
            if (SelectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}
