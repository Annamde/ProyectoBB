using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AddNames
{
    public class NameText : MonoBehaviour
    {
        [SerializeField]
        private Text _textName;
        [SerializeField]
        private GameObject _parent;

        public void DeleteName()
        {
            for (int i = 0; i < GameManager.nameList.Count; i++)
            {
                if (GameManager.nameList.Contains(_textName.text))
                {
                    GameManager.nameList.Remove(_textName.text);
                    
                    Destroy(_parent);
                }
            }
        }
    }
}
