using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiggingSystem : MonoBehaviour{
    [SerializeField] private GameObject m_clickF;
    GameObject[] m_snowList;
	private int m_countSnowDestroy = 0;
	private bool m_inArea = false;
    void Start(){
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}

	private void Update(){
		if(m_inArea){
			Interact();
		}
	}
	void OnTriggerEnter(Collider other){
		m_inArea = true;
		if(m_countSnowDestroy < 4){
			ShowInfo(true);
		}
	}
	void OnTriggerExit(Collider other){
		m_inArea = false;
		ShowInfo(false);
	}
    void ShowInfo(bool status){
        m_clickF.SetActive(status);
    }
	void Interact(){
			if(Input.GetButtonDown("Fire1") && m_countSnowDestroy < 4){
				m_snowList[m_countSnowDestroy].SetActive(false);
				m_countSnowDestroy++;
			}
	}
	void getBelt(int howMuchSnow){
		if(m_countSnowDestroy == howMuchSnow){
			//bierze pasek
		}
	}
}
