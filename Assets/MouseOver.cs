using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour {

    public Menu menu;

    private void OnMouseEnter()
    {
        menu.PlayMove();
    }
}
