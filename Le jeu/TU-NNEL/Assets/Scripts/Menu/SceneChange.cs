using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChange : MonoBehaviour
{
    public void prejeu()
    {
        //Bon comme c'est pas clair du tout voici ce que la fonction fait : En fait, c'est juste pour faire fonctionner le boutton play vuala, je suis pas sûre 
        //sûre qu'on garde ça mdr
        SceneManager.LoadScene("Game");
    }
}
