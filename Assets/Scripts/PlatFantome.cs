using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFantome : MonoBehaviour
{
    SpriteRenderer sprite;
    BoxCollider2D platCollider;
    [SerializeField] GameObject toupie;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        platCollider = GetComponent<BoxCollider2D>();
    }

    public void StartFade(float sec)
    {
        StartCoroutine(Fade(sec));
    }

    public void EnableAll()
    {
        platCollider.enabled = true;
        sprite.enabled = true;
        toupie.SetActive(true);
    }
    public IEnumerator Fade(float sec)
    {
        yield return new WaitForSeconds(sec);

        platCollider.enabled = false;
        sprite.enabled = false;
        toupie.SetActive(false);
        
        StartCoroutine(UnFade());
    }

    public IEnumerator UnFade()
    {
        yield return new WaitForSeconds(3);
        // if already unfaded
        if (platCollider.enabled)
        {
            yield break;
        }
        //else unfade;
        platCollider.enabled = true;
        sprite.enabled = true;
        toupie.SetActive(true);
    }

 
}
