using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPreassurePlate : PressurePlate
{    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable")
        {
            if (col.gameObject.tag == "Player")
            {
                MovingplatformsManager.Instance.PlateNextToPlayer = this;
            }

            float colBottomPosition = col.transform.position.y - col.bounds.size.y / 2;
            float plateUpPosition = transform.position.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
            if (Mathf.Abs(colBottomPosition - plateUpPosition) <= 0.1f)
            {
                entitiesInCollision.Add(col);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable")
        {
            entitiesInCollision.Remove(col);
            if (col.gameObject.tag == "Player")
            {
                MovingplatformsManager.Instance.PlateNextToPlayer = null;
            }
        }
    }

}
