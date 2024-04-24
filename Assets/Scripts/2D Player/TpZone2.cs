using UnityEngine;
using System.Collections;
public class TpZone2 : MonoBehaviour
{
    private Transform tpArrival2;
    private Animator FadeSystem;
    private void Awake()
    {
        tpArrival2 = GameObject.FindGameObjectWithTag("TpArrival2").transform;
        FadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }
    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        FadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.3f);
        collision.transform.position = tpArrival2.position;
    }
}