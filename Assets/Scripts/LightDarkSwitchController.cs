using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkSwitchController : MonoBehaviour
{
    public bool IsDark;
    public Sprite DarkSprite;
    public Sprite LightSprite;
    // Start is called before the first frame update
    void Start()
    {
        IsDark = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipSwitch()
    {
        IsDark = !IsDark;
        var upWhileLightWalls = GameObject.Find("WallsNSuch/WallsUpWhileLight");
        foreach (Transform child in upWhileLightWalls.transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }

        var upWhileDarkWalls = GameObject.Find("WallsNSuch/WallsUpWhileDark");
        foreach (Transform child in upWhileDarkWalls.transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }


        if (gameObject.name == "DarkSwitch")
        {
            if (IsDark)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = DarkSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = LightSprite;
            }
        }
    }
}