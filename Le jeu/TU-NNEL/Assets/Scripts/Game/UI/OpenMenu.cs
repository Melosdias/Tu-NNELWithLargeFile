using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject fiche;

    public void open()
    {
        menu.SetActive(true);
        fiche.SetActive(false);
    }
}
