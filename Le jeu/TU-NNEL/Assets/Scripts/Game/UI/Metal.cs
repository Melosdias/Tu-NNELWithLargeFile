using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metal : MonoBehaviour
{
    [SerializeField] public Text textNumb;
    public static bool update;
    //public static int number;
    // Start is called before the first frame update
    void Start()
    {
        update = true;
        textNumb.text = Ressources.metal.Quant.ToString();
    }

    void Update()
    {
        if(!update)
        {
            textNumb.text = Ressources.metal.Quant.ToString();
            update = true;
        }
    }
}
