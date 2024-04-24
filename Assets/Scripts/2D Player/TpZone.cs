using UnityEngine;
using System.Collections;
public class TpZone : MonoBehaviour
{
    private Transform tpArrival;
    private Animator FadeSystem;
    private void Awake()
    {
        tpArrival = GameObject.FindGameObjectWithTag("TpArrival").transform;
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
        collision.transform.position = tpArrival.position;
    }
}