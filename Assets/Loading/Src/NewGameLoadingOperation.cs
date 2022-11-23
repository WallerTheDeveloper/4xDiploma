using System;
using Core;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class NewGameLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading new game...";
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Globals.Scenes.NEW_GAME, 
                LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(0.7f);
            
            var scene = SceneManager.GetSceneByName(Globals.Scenes.NEW_GAME);
            var newGame = scene.GetRoot<NewGame>();
            onProgress?.Invoke(0.85f);
            newGame.Init();
            
            onProgress?.Invoke(1.0f);
        }
    }
}
