using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AddNames
{
    public class AddNamesController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _nameGameObject;

        [SerializeField]
        private Transform _gridTransform;

        [SerializeField]
        private TMP_InputField _inputFieldText;

        [SerializeField]
        private GameObject _warningText;
        [SerializeField]
        private GameObject _warningOneNameText;
        
        private string _nameText;

        public ScrollRect scroll;

        public void Start()
        {
            if (GameManager.nameList.Count == 0)
            {
                for (int i = 0; i < PlayerPrefsX.GetStringArray("NamesArray").Length; i++)
                {
                    GameManager.nameList.Add(PlayerPrefsX.GetStringArray("NamesArray")[i]);
                }
            }
            for (int i = 0; i < GameManager.nameList.Count; i++)
            {
                SetNewNameText(GameManager.nameList[i]);
            }
        }

        public void SaveTextButton()
        {
            SetWarningText(false);
            _nameText = _inputFieldText.text;
            if (!GameManager.nameList.Contains(_nameText) || _nameText == "" )
            {
                GameManager.nameList.Add(_nameText);
                _inputFieldText.text = "";
                SetNewNameText(_nameText);

                SaveAllNamesInPlayerPrefs();

                StartCoroutine(ScrollDown());
            }
            else
            {
                SetWarningText(true);
            }
        }
        public void SetNewNameText(string name)
        {
            _nameGameObject.GetComponent<Text>().text = name;
            Object.Instantiate(_nameGameObject, _gridTransform);

        }
        
        public void SetWarningText(bool active)
        {
            _warningText.SetActive(active);
            _warningOneNameText.SetActive(false);
        }

        public void SaveAllNamesInPlayerPrefs()
        {
            PlayerPrefsX.SetStringArray("NamesArray", GameManager.nameList.ToArray());
        }

        IEnumerator ScrollDown()
        {
            yield return new WaitForSeconds(.1f);
            scroll.verticalNormalizedPosition = 0f;
        }
    }
}
