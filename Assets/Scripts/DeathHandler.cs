using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NicLib.Health
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] AudioClip deathSound; // need?
        [SerializeField] ShatterObject shatterObject;

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
            
            if (shatterObject)
            {
                shatterObject.Shatter();

            }

            if (shouldAutoDestroy)
            {
                Destroy(this.gameObject, 2f);
            }

            scoreCalculator = GetComponent<ScoreCalculator>();
            if (scoreCalculator)
            {
                scoreCalculator.AddPointsToMischiefOnDestruction();
                scoreCalculator.AddDestructionBonus();
                scoreCalculator.AddToScore_moneyValue();
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
            if (fxSpawnParent)
            {
                fx.transform.parent = fxSpawnParent;

            }
        }

        private void PlayDeathSound()
        {
            if (deathSound != null)
            {
                //AudioSource.PlayClipAtPoint(deathSound, transform.position);
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/destroy/explosion");
            }
        }
        
        public void SetEnableMeshRenderers(bool enabled)
        {
            if (meshesToOffOnDeath.Length == 0) return;
            foreach (MeshRenderer o in meshesToOffOnDeath)
            {
                if (o != null)
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
