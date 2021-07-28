using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInteraction : MonoBehaviour
{
    private Color mouseOverColor = Color.red;
    private Color originalColor;
    private MeshRenderer renderer;
    public Text txt;
    private int bowlCount = 1;
    private int cerealCount = 1;

    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        originalColor = renderer.material.color;
    }

    void OnMouseOver()
    {
        // Change the color of the GameObject to red when the mouse is over GameObject
        renderer.material.color = mouseOverColor;
    }

    void OnMouseDown()
    {
        if (this.tag == "bowl")
        {
            if (bowlCount == 1)
            {
                txt.text = "There is a generous portion of milk inside.";
                bowlCount = 2;
            }
            else
            {
                txt.text = "Do I always pour my milk first? You bet I do.";
                bowlCount = 1;
            }
        }

        else if (this.tag == "cereal")
        {
            if (cerealCount == 1)
            {
                txt.text = "Letter-O's. I've been eating these since I was kid.";
                cerealCount = 2;
            }
            else if (cerealCount == 2)
            {
                txt.text = "I swear they talk to me sometimes.";
                cerealCount = 3;
            }
            else
            {
                txt.text = "Sometimes... Not all the time...";
                cerealCount = 1;
            }
        }

        else if (this.tag == "milk")
        {
            txt.text = "Already out of milk? I just bought some yesterday morning...";
        }
    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        renderer.material.color = originalColor;
    }
}
