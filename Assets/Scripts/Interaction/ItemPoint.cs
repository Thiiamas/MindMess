using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPoint : InteractableComponent
{
    public override void OnInteraction()
    {
        GameObject player = GameObject.Find("NewPlayer");
        Transform itemHUD = player.transform.Find("PlayerCanvas/Item");
        Image image = itemHUD.GetComponent<Image>();

        bool hasItem = Indestructable.instance.hasItem;
        Indestructable.instance.hasItem = !hasItem;
        image.enabled = !hasItem;
    }
}
