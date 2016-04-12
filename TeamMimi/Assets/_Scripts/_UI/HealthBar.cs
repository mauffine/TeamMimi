using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private float m_fillAmount;
    private float m_temphealthval;
    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private GameObject m_player;

    public float MaxValue
    {
        get; set;
    }

    public float Value
    {
        set
        {
            m_fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    private bool once = true;
	// Use this for initialization
	void Start () {
        MaxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        m_temphealthval = m_player.GetComponent<Player_1>().Health;
        Value = m_temphealthval;
        HandleBar();
    }

    private void HandleBar()
    {
        if (m_fillAmount != fill.fillAmount)
        {
            fill.fillAmount = m_fillAmount;
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
