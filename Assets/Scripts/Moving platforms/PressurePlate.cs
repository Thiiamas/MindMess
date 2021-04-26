using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] float pressDistance = -0.3f;

    bool isPressed = false;

    protected List<Collider2D> entitiesInCollision = new List<Collider2D>();

    public bool HasDollInCollision(){
        foreach (Collider2D entity in entitiesInCollision)
        {
            Doll dollComponent = entity.GetComponent<Doll>();
            if(dollComponent != null){
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable")
        {
            if (col.gameObject.tag == "Player")
            {
                MovingplatformsManager.Instance.PlateNextToPlayer = this;
            }

            float colBottomPosition = col.transform.position.y - col.bounds.size.y / 2;
            float plateUpPosition = transform.position.y + transform.GetComponent<Collider2D>().offset.y * transform.localScale.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
             if (Mathf.Abs(colBottomPosition - plateUpPosition) <= 0.1f)
            {
                entitiesInCollision.Add(col);
                if (!isPressed)
                {
                    isPressed = true;
                    MovingplatformsManager.Instance.IncreasePressedPlateCount();
                    MoveAllEntity(new Vector3(0, pressDistance, 0));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable")
        {
            if (col.gameObject.tag == "Player")
            {
                MovingplatformsManager.Instance.PlateNextToPlayer = null;
            }
            if (entitiesInCollision.Count == 1 && entitiesInCollision.Contains(col) && isPressed)
            {
                isPressed = false;
                MovingplatformsManager.Instance.DecreasePressedPlateCount();
                MoveAllEntity(new Vector3(0, -pressDistance, 0));
            }
            entitiesInCollision.Remove(col);
        }
    }

    private void MoveAllEntity(Vector3 translation)
    {
        transform.Translate(translation);
        foreach(Collider2D col in entitiesInCollision)
        {
            col.transform.Translate(translation);
        }
    }
}
