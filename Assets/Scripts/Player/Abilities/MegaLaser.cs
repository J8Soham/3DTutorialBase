using UnityEngine; 

public class MegaLaserAttack : Ability { 
    public override void Use(Vector3 spawnPosition) { 
        RaycastHit[] hits = Physics.SphereCastAll(spawnPosition, 1.0f, transform.forward, m_info.Range); 
        foreach (RaycastHit hit in hits) { 
            if (hit.collider.CompareTag("Enemy")) { 
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_info.Power); 
            } 
        } 
        var emitterShape = cc_ps.shape; 
        emitterShape.length = m_info.Range; 
        cc_ps.Play(); 
    } 
}