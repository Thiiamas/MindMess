using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] float pressDistance = -0.3f;

    bool isPressed = false;

    List<Collider2D> entitiesInCollision = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable")
        {
            entitiesInCollision.Add(col);
            if (!isPressed)
            {
                float colBottomPosition = col.transform.position.y - col.bounds.size.y / 2;
                float plateUpPosition = transform.position.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
                if (Mathf.Abs(colBottomPosition - plateUpPosition) <= 0.1f)
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
            entitiesInCollision.Remove(col);
            if (entitiesInCollision.Count == 0 && isPressed)
            {
                isPressed = false;
                MovingplatformsManager.Instance.DecreasePressedPlateCount();
                MoveAllEntity(new Vector3(0, -pressDistance, 0));
            }
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
