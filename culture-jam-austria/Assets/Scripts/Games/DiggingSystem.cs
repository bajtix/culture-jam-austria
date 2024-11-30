using UnityEngine;
using UnityEngine.UI;

public class DiggingSystem : Interactable {
	[SerializeField] private GameObject m_diggingCanvas;
	[SerializeField] private float m_fillSpeed = 0.2f;
	[SerializeField] private float m_howOften = 0.1f;

	[SerializeField] private Tatzelcam m_camera;
	[SerializeField] private Animator m_animator;
	[SerializeField] private Transform m_a, m_b, m_arms;
	[SerializeField] private GameObject m_armsGfx;
	[SerializeField] private SkinnedMeshRenderer m_renderer;


	private bool m_digDown;
	private float m_progress = 0, m_nextSwitch;


	public override string Tooltip => "Dig up the body";

	private void Start() {
		m_armsGfx.SetActive(false);
		m_diggingCanvas.SetActive(false);
	}

	public override bool CanInteract(Player player) => m_progress < 1;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_progress >= 1;
	public override void InteractionStart(Player player) {
		m_diggingCanvas.SetActive(true);
		player.Controller.AddSpeedModifier("dig", 0f);
		Game.Player.Controller.AddViewModifier("dig", transform.position, 1f);

		m_nextSwitch = m_progress + m_howOften;

		player.Cutscene.AddCamera("dig", m_camera);

		m_armsGfx.SetActive(true);
	}

	public override void InteractionUpdate(Player player) {
		float movement = Game.Input.Digging.Dig.ReadValue<float>() * Time.deltaTime;

		if (m_digDown && Game.Input.Digging.Activate.IsPressed() && movement < 0) {
			m_progress += Mathf.Abs(movement) * m_fillSpeed;
		}

		if (!m_digDown && !Game.Input.Digging.Activate.IsPressed() && movement > 0) {
			m_progress += Mathf.Abs(movement) * m_fillSpeed;
		}

		if (m_progress >= m_nextSwitch) {
			m_nextSwitch = m_progress + m_howOften;
			m_digDown = !m_digDown;
		}

		float fac = !m_digDown ? ((m_nextSwitch - m_progress) / m_howOften) : 1 - ((m_nextSwitch - m_progress) / m_howOften);
		m_animator.SetFloat("anim", fac);

		m_arms.transform.position = Vector3.Lerp(m_a.position, m_b.position, 1 - fac);
		m_renderer.SetBlendShapeWeight(0, m_progress * 100);
		m_renderer.SetBlendShapeWeight(1, m_progress * 100);
	}

	public override void InteractionEnd(Player player) {

		player.Controller.RemoveViewModifier("dig");
		player.Controller.RemoveSpeedModifier("dig");
		m_diggingCanvas.SetActive(false);
		m_armsGfx.SetActive(false);
		player.Cutscene.PopCamera("dig");
	}
}
