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

        bool hasItem = GameManager.Instance.HasItem;
        GameManager.Instance.HasItem = !hasItem;
        image.enabled = !hasItem;
    }
}
