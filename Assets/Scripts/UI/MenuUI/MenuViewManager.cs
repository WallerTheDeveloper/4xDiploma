using System.Collections.Generic;
using UnityEngine;

namespace UI.MenuUI
{
    public class MenuViewManager : MonoBehaviour
    {

        [SerializeField] private MenuView[] _menuViews;
        [SerializeField] private MenuView _initialView;

        public static MenuViewManager MenuViewManagerInstance { get; private set; }

        private MenuView _currentView;
        
        private Stack<MenuView> _viewStack = new Stack<MenuView>(); //FILO - first in last out

        private void Awake() => MenuViewManagerInstance = this;
        private void Start() => InitializeAndHideViews();

        
        private void InitializeAndHideViews()
        {
            foreach (var menuView in _menuViews)
            {
                menuView.Init();
            }

            if (_initialView != null)
            {
                ShowView(_initialView, true);
            }
        }

        private static void RememberCurrentView(bool pushToViewStack)
        {
            var current = MenuViewManagerInstance._currentView;
            if (current != null)
            {
                if (pushToViewStack)
                {
                    MenuViewManagerInstance._viewStack.Push(current);
                }
                current.HideView();
            }
        }

        private static void ShowView(MenuView menuView, bool pushToViewStack)
        {
            RememberCurrentView(pushToViewStack);
            
            menuView.ShowView();

            MenuViewManagerInstance._currentView = menuView;
        }
        
        public static void ShowView<T>() where T : MenuView
        {
            foreach (var menuView in MenuViewManagerInstance._menuViews)
            {
                if (menuView is T)
                {
                    RememberCurrentView(true);
                    menuView.ShowView();
                    MenuViewManagerInstance._currentView = menuView;
                }
            }
        }

        public void ShowPrevious()
        {
            if (MenuViewManagerInstance._viewStack.Count != 0)
            {
                ShowView(MenuViewManagerInstance._viewStack.Pop(), true);
            }
        }
    }
}