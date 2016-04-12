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
       // if (Input.GetMouseButtonDown(0))
       // {
       //    CreateText( Input.mousePosition, Random.Range(0, 20).ToString());
       // }
    }
    public void CreateText(Vector2 a_position, string a_text)
    {
        
        Text tmp = Instantiate(m_text);
        tmp.transform.SetParent(canvas.transform);
        var rectTransform = tmp.transform as RectTransform;
        var screenPos = Camera.main.WorldToScreenPoint(a_position);
        screenPos.x -= Screen.width/2;
        screenPos.y -= Screen.height/2;
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, null, out newPos);
        
        rectTransform.anchoredPosition = newPos;
        Debug.LogFormat("Pos: {0} Screen:{1} Rect:{2}", a_position, screenPos, newPos);

        //tmp.transform.localPosition = a_position;// - new Vector3(Screen.width / 2, Screen.height / 2);
        tmp.text = a_text;
        
    }
}
