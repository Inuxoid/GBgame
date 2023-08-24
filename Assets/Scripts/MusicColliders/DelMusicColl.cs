using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DelMusicColl : MonoBehaviour
{
    
    public void EnemyMusicOn()
        {
            var colliders = GetComponentsInChildren<Collider>(true)
                .Where(c => c.gameObject.name == "MusicCollider")
                .ToList();

            colliders[0]?.gameObject.SetActive(true);

        }

        public void EnemyMusicOf()
        {
            var colliders = GetComponentsInChildren<Collider>(true)
                .Where(c => c.gameObject.name == "MusicCollider")
                .ToList();

            colliders[0]?.gameObject.SetActive(false);
        }

}