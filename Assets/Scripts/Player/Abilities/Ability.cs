using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    #region Cached Components
    protected ParticleSystem cc_ps;
    #endregion

    #region Editor Variables
    [SerializeField] 
    [Tooltip("Damage of the enemy.")]
    protected AbilityInfo m_info;
    #endregion

    #region Private Variables
    #endregion

    #region Initialization
    protected virtual void Awake() {
        cc_ps = GetComponent<ParticleSystem>();
    }
    public abstract void Use(Vector3 spawnPosition);
    #endregion
}
