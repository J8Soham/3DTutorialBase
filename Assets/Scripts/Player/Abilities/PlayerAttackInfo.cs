using UnityEngine;

[System.Serializable]
public class PlayerAttackInfo
{
    
    #region Cached Components
    #endregion

    #region Editor Variables
    [SerializeField] 
    [Tooltip("Name of the attack.")]
    private string m_attackName;
    [SerializeField] 
    [Tooltip("Mapped input button.")]
    // Check input settings for the button.
    private string m_inputButton; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private string m_triggerName; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private GameObject m_abilityGO; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private Vector3 m_offset; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private float m_windupTime; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private float m_frozenTime; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private float m_cooldown; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private int m_healthCost; 
    [SerializeField] 
    [Tooltip("Health of the enemy.")]
    private Color m_attackColor;

    public string AttackName {
        get {
            return m_attackName;
        }
        
    }
    public string Button {
        get {
            return m_inputButton;
        }
        
    }
    public string TriggerName {
        get {
            return m_triggerName;
        }
        
    }
    public GameObject AbilityGo {
        get {
            return m_abilityGO;
        }
        
    }
    public Vector3 Offset {
        get {
            return m_offset;
        }
    }
    public float WindupTime {
        get {
            return m_windupTime;
        }
    }
    public float FrozenTime {
        get {
            return m_frozenTime;
        }
    }
    public int HealthCost {
        get {
            return m_healthCost;
        }
    }
    public Color AttackColor {
        get {
            return m_attackColor;
        }
    }

    #endregion

    #region Public Variables
    public float Cooldown {
        get;
        set;
    }
    public void ResetCooldown() {
        Cooldown = m_cooldown;
    }
    public bool IsReady() {
        return Cooldown <= 0;
    }
    #endregion
}

