using UnityEngine;
using UnityEngine.Animations;

public class MonsterController : StageBehaviour {
	[SerializeField] private Animator m_monsterAnimator;
	[SerializeField] private Tatzelcam m_monsterCam;
	[SerializeField] private SoundBite m_huntStartSound;

	private bool m_dead = false;


	private void Start() {
		m_monsterAnimator.gameObject.SetActive(false);
	}


	private void PlayAnim() {
		m_monsterAnimator.gameObject.SetActive(true);
		m_monsterAnimator.Play("Jumpscare", 0);
		m_monsterAnimator.Play("Jumpscare_camera", 1);

		Invoke(nameof(DeathUI), m_monsterAnimator.GetCurrentAnimatorStateInfo(0).length);
		Game.Player.Controller.enabled = false;
		Game.Player.Cutscene.AddCamera("monsterCam", m_monsterCam);
	}

	public void DeathUI() {
		Game.UI.Dead();
	}

	protected override void OnPlayerDied() {
		if (!m_dead) {
			m_dead = true;
			PlayAnim();
		}
	}

	protected override void OnHuntStarted() {
		Game.SexMan.Play(m_huntStartSound, transform.position - transform.forward, 0.6f);
	}
}
