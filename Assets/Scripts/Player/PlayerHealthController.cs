using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerMovement target;
    public Sprite heartSprite;
    public int numberOfHearts = 3;

    public Image[] _hearts;

    private void Update()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < target.health)
            {
                _hearts[i].color = new Color(1, 1, 1, 1f);
            }
            else
            {
                _hearts[i].color = new Color(1, 1, 1, 0.5f);
            }

            if (i < numberOfHearts)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }
        }
    }
}
