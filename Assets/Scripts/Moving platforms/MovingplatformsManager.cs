using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingplatformsManager : MonoBehaviour
{
    [SerializeField] private GameObject DollPrefab;
    [SerializeField] private GameObject IronDollPrefab;

    private static MovingplatformsManager instance = null;
    
    public static MovingplatformsManager Instance { get { return instance; } }
    public PressurePlate PlateNextToPlayer = null;

    private int pressedPlatesCount = 0;
    private GameObject[] movingPlatforms;
    public GameObject Item { get; set; }

    void Awake()
    {
        // If we don't have an instance set - set it now
        if (!instance)
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        movingPlatforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
    }

    public void IncreasePressedPlateCount()
    {
        pressedPlatesCount++;
        UpdatePlatforms();
    }

    public void DecreasePressedPlateCount()
    {
        pressedPlatesCount--;
        UpdatePlatforms();
    }

    private void UpdatePlatforms()
    {
        foreach (GameObject platform in movingPlatforms)
        {
            platform.GetComponent<MovingPlatformComponent>().OnPressedPlateCountChanged(pressedPlatesCount);
        }
    }

    public void SetItem(bool IsIronDoll)
    {
        GameObject player = GameObject.Find("NewPlayer");
        Image dollImage = player.transform.Find("PlayerCanvas/Doll").GetComponent<Image>();
        Image ironDollImage = player.transform.Find("PlayerCanvas/IronDoll").GetComponent<Image>();

        if (IsIronDoll)
        {
            Item = IronDollPrefab;
        }
        else
        {
            Item = DollPrefab;
        }

        dollImage.enabled = !IsIronDoll;
        ironDollImage.enabled = IsIronDoll;
    }

    public void DropItem()
    {
        if(Item != null && PlateNextToPlayer && !PlateNextToPlayer.HasDollInCollision())
        {
            GameObject player = GameObject.Find("NewPlayer");
            Image dollImage = player.transform.Find("PlayerCanvas/Doll").GetComponent<Image>();
            Image ironDollImage = player.transform.Find("PlayerCanvas/IronDoll").GetComponent<Image>();

            float plateSize = PlateNextToPlayer.transform.GetComponent<BoxCollider2D>().size.y * PlateNextToPlayer.transform.localScale.y;
            float itemSize =  Item.transform.GetComponent<BoxCollider2D>().size.y * Item.transform.localScale.y;
            Vector3 spawnPosition = new Vector3(PlateNextToPlayer.transform.position.x, PlateNextToPlayer.transform.position.y + PlateNextToPlayer.transform.GetComponent<Collider2D>().offset.y + plateSize / 2+ itemSize / 2, 0);

            Instantiate(Item, spawnPosition, Quaternion.identity);
            Item = null;

            dollImage.enabled = false;
            ironDollImage.enabled = false;
        }
    }

    public bool CarryIronDoll()
    {
        if(Item != null)
        {
            return Item.GetComponent<Doll>().IsIronDoll;
        }
        return false;
    }
}
