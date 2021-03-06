﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using Assets.Scripts.Class;


public class ButtonScript : MonoBehaviour{
    public GameObject escapeMenus;
    public GameObject theCharacter;
    public GameObject inventoryMenus;
    public GameObject itemsMenu;
    public GameObject TheCharacter;
    

    public bool WeaponInvOpened;
    public bool ArmorInvOpened;
    public bool AccessInvOpened;
    public bool ShieldInvOpened;

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
        //WeaponInvOpened = false;
        WeaponInvOpened = true;
        ArmorInvOpened = false;
        AccessInvOpened = false;
        ShieldInvOpened = false;

        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach(var weapon in characterClass.Weapons)
        {
             

            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;
            
            var clonedGameObject = Instantiate(gameObjectToClone);
            
            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            rectTrans.anchoredPosition = new Vector3(xToSet,yToSet);
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
        ShieldInvOpened = false;

        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach (var weapon in characterClass.Weapons)
        {
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;

            var clonedGameObject = Instantiate(gameObjectToClone);

            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            rectTrans.anchoredPosition = new Vector3(xToSet, yToSet);
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
        ShieldInvOpened = false;
        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach (var weapon in characterClass.Weapons)
        {
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;

            var clonedGameObject = Instantiate(gameObjectToClone);

            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            rectTrans.anchoredPosition = new Vector3(xToSet, yToSet);
            Text buttonText = ClonedButton.transform.GetChild(0).GetComponent<Text>();
            buttonText.text = weapon.Name;
            //rectTrans.

            xToSet = xToSet + 0;
            yToSet = yToSet + 50;
        }
    }

    public void OpenShieldInventory()
    {
        WeaponInvOpened = false;
        ArmorInvOpened = false;
        AccessInvOpened = false;
        ShieldInvOpened = true;

        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(true);
        var characterClass = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        var xToSet = 0;
        var yToSet = 0;

        foreach (var shield in characterClass.Shields)
        {
            //create a "button and add it to the itemsMenu canvas
            //Cast it as a Button, not a game object
            var gameObjectToClone = itemsMenu.transform.GetChild(0).gameObject;

            var clonedGameObject = Instantiate(gameObjectToClone);

            var rectTrans = clonedGameObject.GetComponent<RectTransform>();
            Button ClonedButton = clonedGameObject.GetComponent<Button>();
            //Button ClonedButton = Instantiate(buttonToClone);
            //Use .SetParent(canvasName,false)    
            clonedGameObject.transform.SetParent(itemsMenu.transform, false);
            rectTrans.anchoredPosition = new Vector3(xToSet, yToSet);
            Text buttonText = ClonedButton.transform.GetChild(0).GetComponent<Text>();
            buttonText.text = shield.Name;
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
            var word = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            foreach (var weapon in characterClass.Weapons)
            {
                weapon.Equipped = false;
                if (weapon.Name == word)
                {
                    weapon.Equipped = true;
                }
            }
            OpenWeaponInventory();
        }
    }
    public void ClickedSave()
    {
        var data = TheCharacter.GetComponent<PlayerController>().thePlayerCharacter;

        Debug.Log(Application.persistentDataPath);
        var path = Path.Combine(Application.persistentDataPath, "SaveGame.txt");

        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }
    
    public void ClickedEnhance()
    {

    }

    public void ClickedLoadGame()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, "SaveGame.txt")))
        {
            using (StreamReader streamReader = File.OpenText(Path.Combine(Application.persistentDataPath, "SaveGame.txt")))
            {
                string jsonString = streamReader.ReadToEnd();

                TheCharacter.GetComponent<PlayerController>().thePlayerCharacter = JsonUtility.FromJson<PlayerCharacter>(jsonString);
            }
        }
        else
        {
            //start over
            SceneManager.LoadScene(1);
        }

    }
}