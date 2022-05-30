using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pop : MonoBehaviour
{
    [SerializeField] public Text textNumb;
    public static bool update;
    
    // Start is called before the first frame update
    void Start()
    {
        update = true;
        textNumb.text = Ressources.population.Quant.ToString();
    }

    void Update()
    {
        if(!update)
        {
            textNumb.text = Ressources.population.Quant.ToString();
            update = true;
        }
    }
}
