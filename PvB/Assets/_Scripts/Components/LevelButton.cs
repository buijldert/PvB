using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(() => OnButtonClicked());
        }

        private void OnButtonClicked()
        {
            Debug.Log("Hoi: " + levelData.DemoText);
        }

        public void SetLevelData(LevelData _data)
        {
            levelData = _data;
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}