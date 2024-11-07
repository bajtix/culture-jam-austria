using UnityEngine;

public abstract class EntityHealth : MonoBehaviour {
    public float maxHealth = 5f;
    protected float m_health;

    private void Start() {
        m_health = maxHealth;
    }

    public virtual void Modify(float howMuch, GameObject source = null) {
        if (howMuch < 0) {
            HealthSubtracted(howMuch, source);
        } else {
            HealthAdded(howMuch, source);
        }

        m_health += howMuch;

        if (m_health <= 0) {
            Died(source);
        }
    }

    protected virtual void HealthSubtracted(float howMuch, GameObject source = null) { }
    protected virtual void HealthAdded(float howMuch, GameObject source = null) { }
    protected virtual void Died(GameObject source) {
        Destroy(gameObject);
    }

}
