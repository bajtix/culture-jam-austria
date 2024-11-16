using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiggingSystem : IInteractable{
    [SerializeField] private GameObject m_panelClickF;
	public SnapController snapController;
    GameObject[] m_snowList;
	private int m_countSnowDestroy = 0;
	private int howMuchSnow = 4;
	private bool m_inArea = false;

	public string Tooltip => "Digging belt";

	void Start() {
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}

	private void Update() {
		if(m_inArea && m_countSnowDestroy < howMuchSnow) {
			Interact();
			ShowInfo(false);
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
			if(snapController.index[m_countSnowDestroy] == 1 && m_countSnowDestroy < howMuchSnow){
				m_snowList[m_countSnowDestroy].SetActive(false);
				m_countSnowDestroy++;
			}
	}

	void getBelt() {
		if(m_countSnowDestroy == howMuchSnow){
			//bierze pasek
		}
	}

	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public bool CanInteract(Player player) {

	}
	public bool CanStopInteraction(Player player) {

	}
	public bool InteractionOver(Player player) {

	}
	public void InteractionStart(Player player) {

	}
	public void InteractionUpdate(Player player) {

	}
	public void InteractionFixedUpdate(Player player) {

	}
	public void InteractionEnd(Player player) {

	}
}
