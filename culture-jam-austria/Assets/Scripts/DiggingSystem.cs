using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class DiggingSystem : MonoBehaviour, IInteractable{
    [SerializeField] private GameObject m_panelClickF;
    private GameObject[] m_snowList;
	private bool m_diggingActivate = false;
	[SerializeField] Image progressBar;
	public string Tooltip => "Digging belt";

	void Start() {
        m_snowList = GameObject.FindGameObjectsWithTag("Snow");
	}
    void ShowInfo(bool status) {
        m_panelClickF.SetActive(status);
    }
	void CheckFillAmount(int numberTab){
		if(progressBar.fillAmount >= 1.0f/m_snowList.LongLength*(numberTab+1)){
			m_snowList[numberTab].SetActive(false);
		}
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
			for(int i = 0; i <= m_snowList.Length; i++){
				CheckFillAmount(i);
			}
		}

	}
	public void InteractionFixedUpdate(Player player) {

	}
	public void InteractionEnd(Player player) {
        ShowInfo(false);
	}
}
