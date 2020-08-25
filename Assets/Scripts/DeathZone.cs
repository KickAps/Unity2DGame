using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Transform mainCamera;

    private Animator fadeSystem;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(respawnPlayer(collision));
        }
    }

    public IEnumerator respawnPlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
        mainCamera.position = playerSpawn.position;

        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(30);
    }
}
