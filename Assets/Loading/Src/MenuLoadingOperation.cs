using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class MenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Main menu loading...";
        
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Globals.Scenes.MAIN_MENU, 
                LoadSceneMode.Additive);
            while (loadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}