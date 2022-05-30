using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    public void close()
    {
        menu.SetActive(false);
    }
}
