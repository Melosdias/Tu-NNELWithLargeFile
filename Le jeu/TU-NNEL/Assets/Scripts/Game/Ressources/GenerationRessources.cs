using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRessources : MonoBehaviour
{
    public static uint nbBatPierre = 0;
    public static uint nbBatIron = 0;
    public static bool hasBeenInvoke = false;
    void Start()
    {
        Batiments.build("base");
        
        foreach (KeyValuePair<string, int> pair in Batiments.DicBat)
        {
            if (pair.Key == "base")
            {
                nbBatPierre++;
                continue;
            }
            if(pair.Key == "Quarry")
            {
                nbBatPierre++;
                continue;
            }
            if(pair.Key == "Mine")
            {
                nbBatIron++;
                continue;
            }
        }
    }
    void Update()
    {
        nbBatPierre = 0;
        nbBatIron = 0;
        foreach (KeyValuePair<string, int> pair in Batiments.DicBat)
        {
            if (pair.Key == "base")
            {
                nbBatPierre++;
                continue;
            }
            if(pair.Key == "Quarry")
            {
                nbBatPierre++;
                continue;
            }
            if(pair.Key == "Mine")
            {
                nbBatIron++;
                continue;
            }
        }
        if(!hasBeenInvoke)
        {
            Invoke("generate", 5);
            hasBeenInvoke = true;
        }
    }

    void generate()
    {
        Ressources.generation("pierre", nbBatPierre);
        hasBeenInvoke = false;
    }
}
