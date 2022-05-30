using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traqueur : MonoBehaviour        //Pour stocker les objets selectionn�s (�a � l'air compliqu� mais pas du tout en fait).
{
    public Dictionary<int, GameObject> TrucsSelectionn�s = new Dictionary<int, GameObject>();

    public void ajouter(GameObject objet)
    {
        int id = objet.GetInstanceID();

        if (!(TrucsSelectionn�s.ContainsKey(id)))
        {
            TrucsSelectionn�s.Add(id, gameObject);
            objet.AddComponent<Lecam�l�on>();
        }
    }

    public void supprimer(int id)
    {
        TrucsSelectionn�s.Remove(id);
    }

    public void tout_supprimer()
    {
        foreach(KeyValuePair<int, GameObject> paire in TrucsSelectionn�s)
        {
            if (paire.Value != null)
            {
                Destroy(TrucsSelectionn�s[paire.Key].GetComponent<Lecam�l�on>());
            }
        }
        TrucsSelectionn�s.Clear();
    }
}
