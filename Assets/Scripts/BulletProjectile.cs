using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>() != null)
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            while (true)
            {
                vfxHitGreen.transform.Translate(other.transform.position);
            }

            StartCoroutine(Destroyvfx());
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private IEnumerator Destroyvfx()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(vfxHitGreen);
    }
}
