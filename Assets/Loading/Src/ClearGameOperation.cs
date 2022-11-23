using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class ClearGameOperation : ILoadingOperation
    {
        public string Description => "Clearing...";

        private readonly ICleanUp _gameCleanUp;

        public ClearGameOperation(ICleanUp gameCleanUp)
        {
            _gameCleanUp = gameCleanUp;
        }

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);

            var loadOp = SceneManager.LoadSceneAsync(Globals.Scenes.MAIN_MENU, LoadSceneMode.Additive);
            while (loadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(0.75f);
           
            var unloadOp = SceneManager.UnloadSceneAsync(_gameCleanUp.SceneName);
            while (unloadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}
