using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowHpBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Canvas hpBar;
    [SerializeField] private GameObject entity;


    void Update()
    {
        if (entity == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            hpBar.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y + 0.2f, hpBar.transform.position.z);
        }
    }
}
