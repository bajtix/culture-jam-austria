using UnityEngine;

public class PlayerHealth : EntityHealth {
	public UIManager uiManager;

	// knockback

	public override void Modify(float howMuch, GameObject source = null) {
		base.Modify(howMuch);
		uiManager.SetHealth(m_health);
	}

	protected override void HealthSubtracted(float howMuch, GameObject source = null) {
		// handle knockback
	}
}
