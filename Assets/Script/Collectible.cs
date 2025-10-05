using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private ParticleSystem particleEffect;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleManager.Instance.CollectItem();

            if (particleEffect != null)
            {
                ParticleSystem effect = Instantiate(particleEffect, transform.position, transform.rotation);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration);
            }

            Destroy(gameObject);
        }
    }
}