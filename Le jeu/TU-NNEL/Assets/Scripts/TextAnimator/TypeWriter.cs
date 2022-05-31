using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    List<string> originaltext = new List<string>();
    Text uiText;
    public Image image;
    public Sprite number1;
    public Sprite number2;
    public Sprite number3;
    public Sprite number4;
    public Sprite number5;

    public float delai = 0.2f;
    void Awake()
    {
        uiText = GetComponent<Text>();
        uiText.text = "";
        originaltext.Add("It is the dawn of the 4th millennium. For more than a hundred centuries no human has even seen the light of day. They live hidden under kilotons of dirt, rocks and ashes, trying in vain to escape a future they most seemingly brought upon themselves.");
        originaltext.Add("They toil, desperately, fighting for resources with survivors they didn’t know existed until yesterday and they fear. They fear that the tunnel they dug a few hours ago is now filled with nameless horrors, they fear that when they come home, they will find everyone they know slaughtered and, above everything else, they fear that they have yet to see the worst this world has to offer.");
        originaltext.Add("Yet even in this rotten carcass of a world, they survive, divided. What little safety exists is assured by the Silos, Ancient cities dating from before the Great Fall, that gather most of the survivors.");
        originaltext.Add("To be a man in such times is to be one amongst untold millions. It is to live in the cruelest and most bloody regime imaginable. These are the tales of those times. Forget the power of technology and science, for so much has been forgotten, never to be re-learned. Forget the promise of progress and understanding, for in the grim dark future there is only war.");
        originaltext.Add("There is no peace under the sky, only an eternity of carnage and slaughter, and the laughter of thirsting gods.");
        StartCoroutine(showLetter());
    }

    IEnumerator showLetter()
    {
        for(int i = 0; i< originaltext.Count; i++)
        {
            uiText.text = "";   
            if (i == 0) image.sprite = number1;
            if (i == 1) image.sprite = number2;
            if (i == 2) image.sprite = number3;
            if (i == 3) image.sprite = number4;
            if (i == 4) image.sprite = number5;
            for(int j = 0; j < originaltext[i].Length; j++)
            {
                uiText.text += originaltext[i][j];
                yield return new WaitForSeconds(delai);
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
    
}
