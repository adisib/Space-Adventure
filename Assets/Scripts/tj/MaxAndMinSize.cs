using UnityEngine;
using System.Collections;

public class MaxAndMinSize : MonoBehaviour {

	public Vector2 Max;
	public Vector2 Min;
	
	private Transform m_objectTransform;
	
	// Use this for initialization
	void Start () {
		m_objectTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	//Max Size
		if(m_objectTransform.localScale.y >= Max.y-.1){
	//		transform.localScale = Max;
		}
		if(m_objectTransform.localScale.x >= Max.x-.1){
	//		transform.localScale = Max;
		}
	
	//Min Size
		if(m_objectTransform.localScale.y <= Min.y+.1){
	//		gameObject.transform.localScale = Min;
		}
		
		if(m_objectTransform.localScale.x <= Min.x+.1){
	//		gameObject.transform.localScale = Min;
		}
	}
}
