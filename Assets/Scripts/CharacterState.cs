[System.Serializable]
public class CharacterState
{
    public CharacterProfile profile;
    public int relationship;

    public CharacterState(CharacterProfile profile)
    {
        this.profile = profile;
        relationship = profile.baseRelationship;
    }
}