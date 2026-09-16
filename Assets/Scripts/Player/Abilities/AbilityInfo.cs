using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    #region Editor Variables
    [SerializeField] 
    [Tooltip("Power of  the ability")] 
    private int m_power; 

    public int Power {
        get {
          return m_power;  
        } 
    }

    [SerializeField] 
    [Tooltip("Max range of the ability")] 
    private float m_range;

    public float Range {
        get {
          return m_range;  
        } 
    }
    #endregion
}


