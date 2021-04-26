using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDog : MonoBehaviour
{
    [SerializeField] private GameObject tear;
    [SerializeField] private float tearFrequencyMin;
    [SerializeField] private float tearFrequencyMax;
    private Vector3 pos1;
    private Vector3 pos2;
    [SerializeField] private float xPos2;
    [SerializeField] private float translateTime;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = this.transform.position;
        pos2 = this.transform.position;
        pos2.x = xPos2;
        StartCoroutine(MoveDog());
        StartCoroutine(Cry());
    }

    IEnumerator MoveDog()
    {
        for (; ; ){
            float t = 0f;
            while (t < translateTime)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(pos1, pos2, Mathf.SmoothStep(0f, t / translateTime, t));
                yield return null;
            }

            t = 0f;
            while (t < translateTime)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(pos2, pos1, Mathf.SmoothStep(0f, t / translateTime, t));
                yield return null;
            }
        }
    }

    IEnumerator Cry(){
        for (; ; )
        {
            Instantiate(tear, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(tearFrequencyMin, tearFrequencyMax));
        }
    }
}
