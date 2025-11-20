using UnityEngine;

[CreateAssetMenu(fileName = "NightProfile", menuName = "RamenGame/Night Profile")]
public class NightProfile : ScriptableObject
{
    public int nightNumber;
    public TextAsset inkJSON;
}