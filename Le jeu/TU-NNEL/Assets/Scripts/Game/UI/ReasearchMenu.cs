using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasearchMenu : MonoBehaviour
{
    [SerializeField] public GameObject menu;

    public void open()
    {
        menu.SetActive(true);
    }
}
