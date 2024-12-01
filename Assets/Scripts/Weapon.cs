using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect; //sound will be used
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public AudioSource weaponSound;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            weaponSound.Play();
            StartCoroutine("Swing");
        }
        else if(type == Type.Range && curAmmo > 0)
        {
            weaponSound.Play();
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.2f); // rest 0.1 second
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        // 2
        yield return new WaitForSeconds(0.3f); ; // rest 0.3 sec// frame
        meleeArea.enabled = false;
        
        // 3
        yield return new WaitForSeconds(0.3f); ; // rest 1 frame
        trailEffect.enabled = false;
    }

    //Use() main routine -> Swing() subroutine -> Use() main routine
    // Coroutine: Use() main routine + Swing() co-routine

    IEnumerator Shot()
    {

        //#1: Bullet shot
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;

        //#2: Exist bullet point
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2,3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

    }
}
