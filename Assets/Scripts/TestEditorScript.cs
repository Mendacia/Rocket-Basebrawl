using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class TestEditorScript
{
    static TestEditorScript()
    {
        Debug.LogWarning("Make sure <b>Player State Reference</b> is set on <b>FieldingPeltingScript(Script)</b> under <b>BunnyTeam!</b>");
        Debug.LogWarning("Make sure <b>Player State Reference</b> is set on <b>ActivatePlayer(Script)</b> under <b>Player Controller!</b>");
        Debug.LogWarning("Make sure <b>Follow</b> and <b>Look At</b> are set on <b>CinemachineVirtualCamera</b> under both <b>CM 3rd Persons!</b>");
    }
}
