using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traqueur : MonoBehaviour        //Pour stocker les objets selectionnés (ça à l'air compliqué mais pas du tout en fait).
{
    public Dictionary<int, GameObject> TrucsSelectionnés = new Dictionary<int, GameObject>();

    public void ajouter(GameObject objet)
    {
        int id = objet.GetInstanceID();

        if (!(TrucsSelectionnés.ContainsKey(id)))
        {
            TrucsSelectionnés.Add(id, gameObject);
            objet.AddComponent<Lecaméléon>();
        }
    }

    public void supprimer(int id)
    {
        TrucsSelectionnés.Remove(id);
    }

    public void tout_supprimer()
    {
        foreach(KeyValuePair<int, GameObject> paire in TrucsSelectionnés)
        {
            if (paire.Value != null)
            {
                Destroy(TrucsSelectionnés[paire.Key].GetComponent<Lecaméléon>());
            }
        }
        TrucsSelectionnés.Clear();
    }
}
