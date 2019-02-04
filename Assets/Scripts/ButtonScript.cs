using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    public GameObject escapeMenus;
    public GameObject theCharacter;
    public GameObject inventoryMenus;
    public GameObject itemsMenu;
    public GameObject TheCharacter;

    public bool WeaponInvOpened;
    public bool ArmorInvOpened;
    public bool AccessInvOpened;

    public void Resume()
    {
        Time.timeScale = 1;
        escapeMenus.SetActive(false);
        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(false);
        theCharacter.GetComponent<PlayerController>().SetPause();
       // theCharacter.GetComponent<PlayerController>().isPaus
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowStatus()
    {

    }

    public void Inventory()
    {
        escapeMenus.SetActive(false);
        inventoryMenus.SetActive(true);
    }

    public void OpenWeaponInventory()
    {
        WeaponInvOpened = true;
        ArmorInvOpened = false;
        AccessInvOpened = false;

        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach(var weapon in characterClass.Weapons)
        {
             

            //Debug.Log("weapons:" + weapon.Name);
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;
            
            var clonedGameObject = Instantiate(gameObjectToClone);
            
            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            Debug.Log("object: " + rectTrans.position);
            rectTrans.anchoredPosition = new Vector3(xToSet,yToSet);
            Debug.Log("object: " + rectTrans.position);
            Text buttonText = ClonedButton.transform.GetChild(0).GetComponent<Text>();
            var equippedText = "";
            if(weapon.Equipped)
            {
                equippedText = "(E)";
            }
            buttonText.text = weapon.Name + equippedText;
            //rectTrans.

            xToSet = xToSet + 0;
            yToSet = yToSet + 50;
        }
    }

    public void OpenArmorInventory()
    {
        WeaponInvOpened = false;
        ArmorInvOpened = true;
        AccessInvOpened = false;

        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach (var weapon in characterClass.Weapons)
        {
            //Debug.Log("weapons:" + weapon.Name);
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;

            var clonedGameObject = Instantiate(gameObjectToClone);

            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            Debug.Log("object: " + rectTrans.position);
            rectTrans.anchoredPosition = new Vector3(xToSet, yToSet);
            Debug.Log("object: " + rectTrans.position);
            Text buttonText = ClonedButton.transform.GetChild(0).GetComponent<Text>();
            buttonText.text = weapon.Name;
            //rectTrans.

            xToSet = xToSet + 0;
            yToSet = yToSet + 50;
        }
    }

    public void OpenAccessoryInventory()
    {
        WeaponInvOpened = false;
        ArmorInvOpened = false;
        AccessInvOpened = true;
        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach (var weapon in characterClass.Weapons)
        {
            //Debug.Log("weapons:" + weapon.Name);
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;

            var clonedGameObject = Instantiate(gameObjectToClone);

            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            Debug.Log("object: " + rectTrans.position);
            rectTrans.anchoredPosition = new Vector3(xToSet, yToSet);
            Debug.Log("object: " + rectTrans.position);
            Text buttonText = ClonedButton.transform.GetChild(0).GetComponent<Text>();
            buttonText.text = weapon.Name;
            //rectTrans.

            xToSet = xToSet + 0;
            yToSet = yToSet + 50;
        }
    }

    public void ClickedItemInInventory()
    {
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        if(WeaponInvOpened)
        {
            foreach (var weapon in characterClass.Weapons)
            {
                var word = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
                Debug.Log(word);

                if(weapon.Name == word)
                {

                }
            }
        }
    }
}