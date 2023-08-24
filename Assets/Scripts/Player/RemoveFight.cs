using System.Collections;
using System.Collections.Generic;
using Let.Foes;
using UnityEngine;

public class RemoveFight : MonoBehaviour
{
    [SerializeField] private int flip = 1;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    //private db[] dbBeh;

    private void Awake()
    {
        //dbBeh = animator.GetBehaviours<db>();
    }

    public void Fliped()
    {
        flip = flip * -1;
    }
    public void Strike()
    {
        animator.SetBool("isPunching", true);
        //Debug.Log("punched");
        foreach (var item in Physics.OverlapBox(new Vector3(this.transform.position.x + (float)flip / 3, this.transform.position.y, this.transform.position.z), 
                                                new Vector3 (0.7f, 0.7f, 0.7f), 
                                                Quaternion.identity, 128))
        {
            item?.GetComponent<Foe>()?.GetDamage(damage);
            item?.GetComponent<Turret>()?.GetStrike(damage);
            item?.GetComponent<Cyborg>()?.GetStrike(damage);
        }
    }   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x + (float)flip / 3, this.transform.position.y, this.transform.position.z),
                                                new Vector3(0.7f, 0.7f, 0.7f));
    }

    //private bool StateCheck()
    //{
    //    foreach (var item in dbBeh)
    //    {
    //        if (item.Exited == false)
    //            return false;
    //    }
    //    return true;
    //}

    private void Update()
    {

    }
}
