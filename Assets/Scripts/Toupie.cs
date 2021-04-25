using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toupie : MonoBehaviour
{
    [SerializeField] Transform startingPoint;

    [SerializeField] Transform xGauche;
    [SerializeField] Transform xDroite;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float dashSpeed;
    Transform playerTransform;
    Rigidbody2D rb;
    public bool isDashing = false;
    public float widthPlatform;
    public string dashDirection;
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        widthPlatform = Mathf.Abs(xGauche.position.x - xDroite.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        RaycastHit2D checkLeft;
        RaycastHit2D checkRight;
        checkLeft = Physics2D.Raycast(transform.position, left, widthPlatform, layerMask);
        checkRight = Physics2D.Raycast(transform.position, right, widthPlatform, layerMask);
        if (checkLeft && !isDashing)
        {
            isDashing = true;
            dashDirection = "left";
            
        } else if (checkRight && !isDashing)
        {
            
            isDashing = true;
            dashDirection = "right";
        }

        if (isDashing && dashDirection != "")
        {
            if (transform.position.x + 0.2f < xGauche.position.x)
            {
                transform.position = xGauche.position;
                isDashing = false;
                dashDirection = "";
                rb.velocity = Vector3.zero;
            } else if (transform.position.x - 0.2f > xDroite.position.x)
            {
                transform.position = xDroite.position;
                isDashing = false;
                dashDirection = "";
                rb.velocity = Vector3.zero;
            }

            if (dashDirection == "left" && transform.position.x > xGauche.position.x)
            {
                rb.velocity = left * dashSpeed;
            } else if (dashDirection == "right" && transform.position.x < xDroite.position.x)
            {
                rb.velocity = right * dashSpeed;
            }
        }

        if (rb.velocity == Vector2.zero && isDashing)
        {
            isDashing = false;
        }

    }
}
