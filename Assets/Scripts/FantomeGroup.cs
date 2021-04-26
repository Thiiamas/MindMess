using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomeGroup : MonoBehaviour
{
    PlatFantome last;
    PlatFantome current;

    List<PlatFantome> lasts;
    public int X;
    PlayerController playerController;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lasts = new List<PlatFantome>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = playerController.CheckFantome(layerMask);
        if (hit.transform)
        {
            current = hit.transform.gameObject.GetComponent<PlatFantome>();
        }

        if (current && !hit.transform)
        {
            if (lasts.Count >= X)
            {
                lasts[0].EnableAll();
                lasts.RemoveAt(0);
            }
            lasts.Add(current);
            last = current;
            current = null;
            last.StartFade(0);

        }
    }

}
