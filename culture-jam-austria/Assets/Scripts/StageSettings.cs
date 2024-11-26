using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSettings", menuName = "Game/StageSettings")]
public class StageSettings : ScriptableObject {
    [InfoBox("This gets set automatically", EInfoBoxType.Warning)]
    [ReadOnly] public int stageIndex;

    [InfoBox("The minimal level of the storm. Remember to subtract that when measuring times.")]
    public float stormMinTime = 0f;
    [InfoBox("How much faster does it take for the snow to recede than to progress")]
    public float stormRecessionRate = 1.5f;

    [InfoBox("How long does it take for the storm to start receeding/proceeding")]
    public float stormDirectionChangeTime = 5;

    [InfoBox("When the storm reaches this level, the monster hunt starts - there is no coming back, it WILL progress to the next stage")]
    public float huntingStartStormTime = 20;

    [InfoBox("When the storm reaches this level, the player cannot survive outside of the shelter")]
    public float deadlyStormTime = 40;

    [InfoBox("When the storm reaches this level, we advance to the next stage and allow the storm to recede.")]
    public float huntingEndStormTime = 50;

}
