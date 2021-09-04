using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WBTransition.SampleScene
{


    /// <summary>
    /// �{�^���������ꂽ�Ƃ��Ɏw��̃V�[���ɑJ�ڂ���T���v���R�[�h
    /// </summary>
    public class SceneChangeSample : MonoBehaviour
    {
        [Header("�J�ڐ�̃V�[��")]
        [SerializeField] private WBTransition.Scene nextScene;

        [Header("�`�������ϐ�")]
        [SerializeField] private InputField field;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Slider slider;


        public void ButtonClicked()
        {
            var d = new Dictionary<string, object>()
            {
                {"inputField",field.text },
                {"toggle",toggle.isOn },
                {"slider",slider.value },
            };

            //�V�[���J�ڂ��J�n
            WBTransition.SceneManager.LoadScene(nextScene,d,null);
        }

    }
}
