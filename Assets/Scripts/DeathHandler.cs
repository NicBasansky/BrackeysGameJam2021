using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NicLib.Health
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] AudioClip deathSound; // need?

        [SerializeField] GameObject deathFxPrefab;
        [SerializeField] Transform fxSpawnParent;
        
        [SerializeField] MeshRenderer[] meshesToOffOnDeath;
        [SerializeField] Transform parentGOToOffChildrenOnDeath;

        [SerializeField] bool shouldAutoDestroy = true;

        ScoreCalculator scoreCalculator;


        public void Kill()
        {
            SpawnDeathFX();
            SetEnableMeshRenderers(false);
            SetAllChildrenActive(false);
            PlayDeathSound();

            if (shouldAutoDestroy)
            {
                Destroy(this.gameObject, 2f);
            }

            scoreCalculator = GetComponent<ScoreCalculator>();
            if (scoreCalculator)
            {
                scoreCalculator.AddPointsToMischiefOnDestruction();
            }
        }
        
        public void SpawnDeathFX()
        {
            if (deathFxPrefab == null || fxSpawnParent == null)
            {
                Debug.Log("Death FX properties are null");
                return;
            }
            GameObject fx = Instantiate(deathFxPrefab, transform.position, Quaternion.identity);
            fx.transform.parent = fxSpawnParent;
        }

        private void PlayDeathSound()
        {
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
        }
        
        public void SetEnableMeshRenderers(bool enabled)
        {
            if (meshesToOffOnDeath.Length == 0) return;
            foreach (MeshRenderer o in meshesToOffOnDeath)
            {
                o.enabled = enabled;
            }
        }

        public void SetAllChildrenActive(bool enabled)
        {
            if (parentGOToOffChildrenOnDeath == null) return;
            foreach (Transform go in parentGOToOffChildrenOnDeath)
            {
                go.gameObject.SetActive(false);
            }
        }
        
       
    }

}
