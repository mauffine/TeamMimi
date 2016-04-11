using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private Text m_text;
    Canvas canvas;
    // Use this for initialization
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           CreateText( Input.mousePosition, Random.Range(0, 20).ToString());
        }
    }
    public void CreateText(Vector3 a_position, string a_text)
    {
        Text tmp = Instantiate(m_text);
        tmp.transform.SetParent(canvas.transform);
        tmp.transform.localPosition = a_position - new Vector3(Screen.width / 2, Screen.height / 2);
        tmp.text = a_text;
    }
}
