using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndWriter : MonoBehaviour
{
    [SerializeField] private string text;
    Text uiText;
    public float delai = 0.2f;
    void Awake()
    {
        uiText = GetComponent<Text>();
        uiText.text = "";
        StartCoroutine(showLetter());
    }

    IEnumerator showLetter()
    {
        foreach(char car in text)
        {
            uiText.text += car;
            yield return new WaitForSeconds(delai);
            
        }
        
    }
}
