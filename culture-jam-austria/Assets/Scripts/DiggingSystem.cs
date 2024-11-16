using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiggingSystem : MonoBehaviour{
    [SerializeField] private GameObject m_diggingArea;
    [SerializeField] private GameObject m_clickF;
	[SerializeField] private GameObject m_Player;
	public GameObject snow;
	public GameObject snow2;
	public GameObject snow3;
	int countSnowDestroy;
	bool inArea = false;
	private void Update()
	{
		if(inArea){
			ShowInfo();
			Interact();
			Interact2();
			Interact3();
		}
	}
	void OnTriggerEnter(Collider other){
		inArea = true;
	}
	void OnTriggerExit(Collider other)
	{
		inArea = false;
	}
    void ShowInfo(){
        m_clickF.SetActive(true);
    }
	void Interact(){
		if(Input.GetButton("Fire1")) {
			Destroy(snow);
			countSnowDestroy++;
		}
	}
	void Interact2(){
		if(Input.GetButton("Fire2")) {
			Destroy(snow2);
			countSnowDestroy++;
		}
	}
	void Interact3(){
		if(Input.GetButton("Fire3")) {
			Destroy(snow3);
			countSnowDestroy++;
		}
	}
	void getBelt(){
		if(countSnowDestroy == 3){
			//bierze pasek
		}
	}
}
