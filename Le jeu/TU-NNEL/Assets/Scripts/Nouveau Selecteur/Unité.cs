using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unité : MonoBehaviour
{
    void Start()
    {
        UnitSelection.Instance.unités.Add(this.gameObject);

    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unités.Remove(this.gameObject);
    }
}
