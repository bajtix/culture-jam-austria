using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiggingSystem : MonoBehaviour, IInteractable{
    [SerializeField] private GameObject m_panelClickF;
	public SnapController snapController;
    GameObject[] m_snowList;
	private int m_countSnowDestroy = 0;
	private int howMuchSnow = 4;
	private bool m_inArea = false;
	private bool m_diggingActivate = false;
	public string Tooltip => "Digging belt";

	void Start() {
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}

	private void Update() {
		if(m_diggingActivate) {
			print("xd");
		}
		Input.GetAxis("Mouse Y");
	}

	void OnTriggerEnter(Collider other) {
		m_diggingActivate = true;
	}

	void OnTriggerExit(Collider other) {
		m_diggingActivate = false;
	}

    void ShowInfo(bool status) {
        m_panelClickF.SetActive(status);
    }

	void Interact() {
			if(Input.GetKeyDown(KeyCode.Space) && m_countSnowDestroy < howMuchSnow){
				m_snowList[m_countSnowDestroy].SetActive(false);
				m_countSnowDestroy++;
			}
	}

	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public bool CanInteract(Player player) {
		return true;
	}
	public bool CanStopInteraction(Player player) {
	    return true;
    }
    bool IInteractable.InteractionOver(Player player) {
        return false;
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
