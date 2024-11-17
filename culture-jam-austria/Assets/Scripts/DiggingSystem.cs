using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class DiggingSystem : MonoBehaviour, IInteractable{
    [SerializeField] private GameObject m_panelClickF;
    GameObject[] m_snowList;
	private bool m_diggingActivate = false;

	[SerializeField] Image progressBar;
	public string Tooltip => "Digging belt";

	void Start() {
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}

    void ShowInfo(bool status) {
        m_panelClickF.SetActive(status);
    }

	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public bool CanInteract(Player player) {
		return !m_diggingActivate;
	}
	public bool CanStopInteraction(Player player) {
	    return true;
    }
    bool IInteractable.InteractionOver(Player player) {
        return false;
	}
	public void InteractionStart(Player player) {
        print("Interaction start");
        ShowInfo(true);
	}
	public void InteractionUpdate(Player player) {
		if(Input.GetMouseButton(0)) {
			progressBar.fillAmount += Math.Abs(Input.GetAxis("Mouse Y") * Time.deltaTime);
			if(progressBar.fillAmount > 0.25){
				m_snowList[0].SetActive(false);
			}
			if(progressBar.fillAmount > 0.5){
				m_snowList[1].SetActive(false);
			}
			if(progressBar.fillAmount > 0.75){
				m_snowList[2].SetActive(false);
			}
			if(progressBar.fillAmount >= 1){
				m_snowList[3].SetActive(false);
			}
		}

	}
	public void InteractionFixedUpdate(Player player) {

	}
	public void InteractionEnd(Player player) {
        ShowInfo(false);
	}
}
