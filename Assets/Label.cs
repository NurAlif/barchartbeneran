using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Label : MonoBehaviour
{
    static public int ALIGN_CENTER = 0;
    static public int ALIGN_RIGHT = 1;
    static public int ALIGN_LEFT = 2;

    [SerializeField]
    private TextMeshPro tmpLabel;
    
    public void SetLabel(string text)
    {
        tmpLabel.SetText(text);
        SetAlign(ALIGN_RIGHT);
    }

    public void SetAlign(int align)
    {
        if (align == ALIGN_RIGHT)
        {
            tmpLabel.margin = new Vector4(5f, 1.5f, 10f, 1.5f);
            tmpLabel.alignment = TextAlignmentOptions.Right;
        }
        else if (align == ALIGN_LEFT)
        {
            tmpLabel.margin = new Vector4(10f, 1.5f, 5f, 1.5f);
            tmpLabel.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            tmpLabel.margin = new Vector4(5f, 1.5f, 5f, 1.5f);
            tmpLabel.alignment = TextAlignmentOptions.Center;
        }
    }
}
