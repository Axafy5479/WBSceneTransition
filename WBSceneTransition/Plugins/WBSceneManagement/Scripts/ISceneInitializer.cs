using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WBSceneManagement
{
    /// <summary>
    /// シーン遷移した際に始めに起動する
    /// </summary>
    public interface ISceneInitializer
    {
        /// <summary>
        /// マスクを開く直前に呼ばれる
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args);

        /// <summary>
        /// マスクを開ききった後に呼ばれる
        /// </summary>
        /// <param name="args"></param>
        public void AfterOpenMask(Dictionary<string, object> args);


    }

}
