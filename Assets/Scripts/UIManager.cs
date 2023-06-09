using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject mainInventory, hotBar, componentsIventory, armorInventory, descriptionUI;
    bool inventoryOn;

    void Start()
    {
        inventoryOn = true;
        ShowInventory();
    }

    public void ShowInventory()
    {
        if (!inventoryOn)
        {
            LeanTween.moveLocalY(inventoryParent, -55, 0.3f).setEaseOutBack().setOnComplete(() => 
            {
                LeanTween.moveLocalY(componentsIventory, 64.5f, 0.3f).setEaseInOutQuint();
                LeanTween.moveLocalY(armorInventory, 74.5f, 0.3f).setEaseInOutQuint();
            });
            inventoryOn = true;
            return;
        }       
        else
        {
            LeanTween.moveLocalY(componentsIventory, 18.5f, 0.3f).setEaseInOutQuint();
            LeanTween.moveLocalY(armorInventory, 7.5f, 0.3f).setEaseInOutQuint().setOnComplete(() =>
            {
                LeanTween.moveLocalY(inventoryParent, -149, 0.3f).setEaseOutBack();
            }); 
           
            inventoryOn = false;
            return;
        }
    }

    public void ShowDescription()
    {
        LeanTween.moveLocalX(descriptionUI, -75.5f, 0.15f).setEaseInOutQuint().setOnComplete(() =>
        {
            LeanTween.moveLocalX(descriptionUI, -141.5f, 0.3f).setEaseOutBack();
        });
    }

    public void HideDescription()
    {
        LeanTween.moveLocalX(descriptionUI, -75.5f, 0.3f).setEaseInOutQuint();
    }
}
