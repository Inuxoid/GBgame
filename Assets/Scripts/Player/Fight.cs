using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private int flip = 1;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    public void Fliped()
    {
        flip = flip * -1;
    }
    public void Strike()
    {
        animator.SetBool("isPunching", true);
        foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + flip, this.transform.position.y), 
                                                new Vector3 (0.7f, 0.7f, 0.7f), 
                                                Quaternion.identity, 128))
        {
            item?.GetComponent<Enemy>()?.GetStrike(damage);
            item?.GetComponent<Turret>()?.GetStrike(damage);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") == 0)
        {
            if (!gameObject.GetComponent<PlayerMovement>().Crouch)
            {
                Strike();
            }
        }
    }
}
