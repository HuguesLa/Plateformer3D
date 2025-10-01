using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Vitesse de rotation en degrés par seconde (ajustable dans l'Inspector)
    [SerializeField] private ParticleSystem particleEffect; // Prefab de l'effet de particules à instancier à la destruction

    // Fonction appelée à chaque frame
    private void Update()
    {
        // Fait tourner l'objet lentement autour de l'axe Y
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    // Fonction appelée quand un trigger entre en contact (version 3D)
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet en collision est le joueur (via son tag)
        if (other.CompareTag("Player"))
        {
            // Incrémente le compteur via le manager
            CollectibleManager.Instance.CollectItem();

            // Instancie et joue l'effet de particules si assigné
            if (particleEffect != null)
            {
                ParticleSystem effect = Instantiate(particleEffect, transform.position, transform.rotation);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration); // Détruit l'effet après sa durée
            }

            // Détruit le collectible
            Destroy(gameObject);
        }
    }
}