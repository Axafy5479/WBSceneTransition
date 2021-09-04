using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WBTransition.SampleScene
{

    /// <summary>
    /// �V�[���J�ڌ�ɌĂ΂�郁�\�b�h�����������N���X�̃T���v��
    /// </summary>
    public class InitializerSample : MonoBehaviour, ISceneInitializer
    {
        [SerializeField] private Text inputResult;
        [SerializeField] private Text toggleResult;
        [SerializeField] private Text sliderResult;

        [SerializeField] private GameObject sceneChangeButton;


        /// <summary>
        /// �V�[���J�ڌ�A�}�X�N���J���n�߂�O��
        /// ���̃��\�b�h���Ă΂��
        /// </summary>
        /// <param name="args">�J�ڑO�̃V�[������`����ꂽ�ϐ�</param>
        /// <returns></returns>
        public void AfterOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("AfterOpen");

            sceneChangeButton.SetActive(true);
        }

        /// <summary>
        /// �}�X�N���J���I���������ɌĂ΂��
        /// </summary>
        /// <param name="args">�J�ڑO�̃V�[������`����ꂽ�ϐ�</param>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("BeforOpen");

            var s = (string)args["inputField"];
            var b = (bool)args["toggle"];
            var n = (float)args["slider"];

            inputResult.text = $"���͂��ꂽ�����́u{s}�v�ł�";
            toggleResult.text = $"�X�C�b�`��{(b ? "on" : "off")}�ł�";
            sliderResult.text = $"�X���C�_�[�̒l��{n}�ł�";

            sceneChangeButton.SetActive(false);

            yield return null;
        }


    }
}

