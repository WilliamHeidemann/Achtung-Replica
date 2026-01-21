using UnityEngine;
using UnityEngine.UIElements;

public class ScoreboardUILogic : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject frame;
    
    public void Toggle(bool show)
    {
        uiDocument.enabled = show;
        frame.SetActive(show);
    }
}
