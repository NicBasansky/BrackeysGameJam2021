using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NicLib.Health
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] string eventPathDestructionSound; // need?
        [SerializeField] AudioClip[] deathSound;
        [SerializeField] ShatterObject shatterObject;

        [SerializeField] GameObject deathFxPrefab;
        [SerializeField] GameObject[] extraDeathFxPrefabs;
        [SerializeField] Transform fxSpawnParent;
        
        [SerializeField] MeshRenderer[] meshesToOffOnDeath;
        [SerializeField] Transform parentGOToOffChildrenOnDeath;

        [SerializeField] bool shouldAutoDestroy = true;
        [SerializeField] bool shouldNeverDespawn = false;

        ScoreCalculator scoreCalculator;


        public void Kill()
        {
            SpawnDeathFX();
            SetAllChildrenActive(false);
            PlayDeathSound();
            if (!shouldNeverDespawn)
            {
                SetEnableMeshRenderers(false);
                
            
            }

            if (shatterObject)
            {
                shatterObject.Shatter();

            }

            if (shouldAutoDestroy && !shouldNeverDespawn)
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
            if (deathFxPrefab == null) 
            {
                Debug.Log("Death FX properties are null");
                return;
            }
            GameObject fx = Instantiate(deathFxPrefab, transform.position, Quaternion.identity);
            if (fxSpawnParent)
            {
                fx.transform.parent = fxSpawnParent;
            }
            //fx.transform.position = transform.position;
            
            
            foreach(GameObject go in extraDeathFxPrefabs)
            {
                GameObject extraFx = Instantiate(go, transform.position, Quaternion.identity);
                //if (fxSpawnParent)
                {
                    extraFx.transform.parent = transform;//xSpawnParent;
                }
            }
            

        }

        private void PlayDeathSound()
        {
            if (eventPathDestructionSound != string.Empty)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:" + eventPathDestructionSound);
            }
            else if (deathSound.Length != 0)
            {
                int randomIndex = Random.Range(0, deathSound.Length - 1);
                if (deathSound[randomIndex])
                    AudioSource.PlayClipAtPoint(deathSound[randomIndex], transform.position);

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
