using UnityEngine;

public class SkillTree : MonoBehaviour
{
    private SkillTreeButton buttonSelected;

    public void SelectButton(SkillTreeButton button)
    {
        if(buttonSelected != null)
        {
            buttonSelected.SetSelected(false);
        }

        buttonSelected = button;
        buttonSelected.SetSelected(true);
    }
}
