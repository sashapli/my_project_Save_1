using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] InputField integerText;
        [SerializeField] InputField stringifyText;
        [SerializeField] Toggle[] booleanArrayToggle;


        public void Load() => YandexGame.LoadProgress();

    }
}