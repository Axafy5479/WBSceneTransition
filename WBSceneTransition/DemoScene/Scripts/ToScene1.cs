using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBTransition.SampleScene
{
    public class ToScene1 : MonoBehaviour
    {
        [SerializeField] private Scene scene1;
        public void ButtonClicked()
        {
            SceneManager.LoadScene(scene1);
        }
    }
}
