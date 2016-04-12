using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Animate(int a_animNum)
    {
        switch (a_animNum)
        {
            case 0:
                gameObject.GetComponent<Animator>().SetTrigger("Idle");
                break;
            case 1:
                gameObject.GetComponent<Animator>().SetTrigger("Run");
                break;
            default:
                break;
        }
    }
}
