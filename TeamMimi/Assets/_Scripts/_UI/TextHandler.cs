using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour {
    [SerializeField]
    private float m_lifetime = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().color *= m_lifetime;
        m_lifetime -= .1f * Time.deltaTime;
        if (m_lifetime <= 0)
            Destroy(gameObject);
        gameObject.transform.Translate(new Vector3(0, 15 * Time.deltaTime, 0));
	}
}
