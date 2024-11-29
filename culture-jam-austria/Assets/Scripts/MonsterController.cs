using UnityEngine;
using UnityEngine.Animations;

public class MonsterController : MonoBehaviour
{
	[SerializeField] private Animator m_animator_monster;
	[SerializeField] private Tatzelcam m_monster_cam;
	[SerializeField] private GameObject m_game_over_screen;

	private bool dead = false;
	private bool flipflop = false;

    void Start()
    {
		m_animator_monster.gameObject.SetActive(false);
		m_game_over_screen.SetActive(false);
	}

    // Update is called once per frame
    void LateUpdate()
    {
		if(dead && m_animator_monster.GetCurrentAnimatorStateInfo(0).IsName("nic")) {
			m_game_over_screen.SetActive(true);
		}

		//if (Game.Input.Player.Jump.WasPressedThisFrame()) {
		//	if (flipflop) {
		//		m_animator_monster.gameObject.SetActive(false);
		//	} else {
		//		PlayAnim();
		//	}
		//	flipflop = !flipflop;
		//}
	}

	private void PlayAnim() {
		m_animator_monster.gameObject.SetActive(true);
		m_animator_monster.Play("Jumpscare", 0);
		m_animator_monster.Play("Jumpscare_camera", 1);
		Game.Player.Cutscene.AddCamera("monsterCam", m_monster_cam);
	}

	public void Die() {
		if (!dead) {
			dead = true;
			PlayAnim();
		}
	}
}
