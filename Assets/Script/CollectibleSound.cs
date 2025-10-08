using UnityEngine;

   public class CollectibleSound : MonoBehaviour
   {
       [SerializeField] private AudioClip collectSound;

       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               if (collectSound != null)
               {
                   // Crée un objet temporaire pour jouer le son
                   GameObject tempAudioObject = new GameObject("TempAudio");
                   AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
                   tempAudioSource.clip = collectSound;
                   tempAudioSource.playOnAwake = false;
                   tempAudioSource.Play();
                   Debug.Log("Son joué : " + collectSound.name);
                   Destroy(tempAudioObject, collectSound.length);
               }
               else
               {
                   Debug.LogWarning("CollectibleSound : AudioClip manquant !");
               }
           }
       }
   }