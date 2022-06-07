using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unités = new List<GameObject>();
    public List<GameObject> unités_selectionnées = new List<GameObject>();
    public Queue<GameObject> mineurs = new Queue<GameObject>(); //Rendu static pour pouvoir modifier la pile dans d'autre script
    public List<GameObject> mines = new List<GameObject>();

    private static UnitSelection _instance;
    public static UnitSelection Instance { get { return _instance; } }       //juste pour pouvoir utiliser le script et ses fonctions ailleurs
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void ClickSelect(GameObject Unité)
    {
        if(Unité.tag == "mine")
        {
            DeselectAll();
            unités_selectionnées.Add(Unité);
            Unité.transform.GetChild(0).gameObject.SetActive(true);
            Unité.GetComponent<Bouge>().enabled = true;
        }
    }

    public void ShiftClickSelect(GameObject Unité)
    {
        if(Unité.tag == "mine")
        {
            if (!unités_selectionnées.Contains(Unité))
            {
                unités_selectionnées.Add(Unité);
                Unité.transform.GetChild(0).gameObject.SetActive(true);
                Unité.GetComponent<Bouge>().enabled = true;
            }
            else
            {
                Unité.GetComponent<Bouge>().enabled = false;
                Unité.transform.GetChild(0).gameObject.SetActive(false);
                unités_selectionnées.Remove(Unité);
            }
        }
    }

    public void DragSelect(GameObject Unité)
    {
        if(Unité.tag == "mine")
        {
            if (!unités_selectionnées.Contains(Unité))
            {
                unités_selectionnées.Add(Unité);
                Unité.transform.GetChild(0).gameObject.SetActive(true);
                Unité.GetComponent<Bouge>().enabled = true;
            }
        }
    }

    public void Deselect(GameObject unité)
    {

    }

    public void DeselectAll()
    {
        foreach (GameObject Unité in unités_selectionnées)
        {
            Unité.GetComponent<Bouge>().enabled = false;
            Unité.transform.GetChild(0).gameObject.SetActive(false);
        }
        unités_selectionnées.Clear();
    }
    private void Update()
    {
        mines.Clear();
        foreach (GameObject l in mineurs)
        {
            mines.Add(l);
        }
    }
}
