using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private int flip = 1;
    [SerializeField] private int damage;
    public void Fliped()
    {
        flip = flip * -1;
    }
    public void Strike()
    {
        foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + flip, this.transform.position.y), 
                                                new Vector3 (1, 1, 1), 
                                                Quaternion.identity, 128))
        {
            item.GetComponent<Enemy>().GetStrike(damage);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Strike();
        }
    }
}
