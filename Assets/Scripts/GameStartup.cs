using System;
using System.Collections;
using System.Collections.Generic;
using Loading;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    private LoadingScreenProvider LoadingScreenProvider => ProjectContext.Instance.LoadingScreenProvider;

    private void Start()
    {
        ProjectContext.Instance.Initialize();

        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new MenuLoadingOperation());
        LoadingScreenProvider.LoadAndDestroy(loadingOperations);
    }
}
