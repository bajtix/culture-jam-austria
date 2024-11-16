using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiggingSystem : MonoBehaviour{
    [SerializeField] private GameObject m_panelClickF;
    GameObject[] m_snowList;
	private int m_countSnowDestroy = 0;
	private int howMuchSnow = 4;
	private bool m_inArea = false;

    void Start() {
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}

	private void Update() {
		if(m_inArea && m_countSnowDestroy < howMuchSnow) {
			Interact();
			ShowInfo(true);
		}else {
			ShowInfo(false);
		}
	}

	void OnTriggerEnter(Collider other) {
		m_inArea = true;

	}

	void OnTriggerExit(Collider other) {
		m_inArea = false;
		ShowInfo(false);
	}

    void ShowInfo(bool status) {
        m_panelClickF.SetActive(status);
    }

	void Interact() {
			Cursor.lockState = CursorLockMode.None;
			if(Input.GetButtonDown("Fire1") && m_countSnowDestroy < howMuchSnow){
				m_snowList[m_countSnowDestroy].SetActive(false);
				m_countSnowDestroy++;
			}
	}

	void getBelt() {
		if(m_countSnowDestroy == howMuchSnow){
			//bierze pasek
		}
	}
}
