using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private int flip = 1;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    private db[] dbBeh;

    private void Awake()
    {
        dbBeh = animator.GetBehaviours<db>();
    }

    public void Fliped()
    {
        flip = flip * -1;
    }
    public void Strike()
    {
        animator.SetBool("isPunching", true);
        Debug.Log("punched");
        foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + flip, this.transform.position.y), 
                                                new Vector3 (0.7f, 0.7f, 0.7f), 
                                                Quaternion.identity, 128))
        {
            item?.GetComponent<Enemy>()?.GetStrike(damage);
            item?.GetComponent<Turret>()?.GetStrike(damage);
        }
    }

    private bool StateCheck()
    {
        foreach (var item in dbBeh)
        {
            if (item.Exited == false)
                return false;
        }
        return true;
    }

    private void Update()
    {

    }
}
