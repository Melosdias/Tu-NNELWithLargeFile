using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCore : MonoBehaviour
{
    [SerializeField] public Text textNumb;
    public static bool update;
    //public static int number;
    // Start is called before the first frame update
    void Start()
    {
        update = true;
        textNumb.text = Ressources.coeurEnergie.Quant.ToString();
    }

    void Update()
    {
        if(!update)
        {
            textNumb.text = Ressources.coeurEnergie.Quant.ToString();
            update = true;
        }
    }
}
