using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Services.Debugging
{
#if UNITY_EDITOR
    /// <summary>
    /// DebugInputService работает с атрибутом DebugKey(AnyKey)
    /// позволяет вызывать методы помеченые таким атрибутом по нажатию клавиши
    /// работает ТОЛЬКО в UnityEditor
    /// НЕ ЗАБУДЬ - Сервис нужно зарегистрировать на уровне проекта
    /// </summary>
    public class DebugInputService : IInitializable, IDisposable
    {
        private DebugInputActions _inputActions;
        private readonly Dictionary<Key, List<MethodInfo>> _debugMethods = new();

        public void Initialize()
        {
            _inputActions = new DebugInputActions();
            _inputActions.Debug.Enable();

            // Регистрируем действия для отслеживаемых клавиш
            _inputActions.Debug.DebugAction.performed += ctx => CheckAndInvoke(Key.T);

            // Ищем методы, помеченные DebugKeyAttribute
            RegisterDebugMethods();
        }

        private void RegisterDebugMethods()
        {
            // Получаем сборку, содержащую пользовательский код
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");

            if (assembly == null)
            {
                Debug.LogWarning("Assembly-CSharp not found. Debug methods registration skipped.");
                return;
            }

            // Ищем методы с DebugKeyAttribute только в Assembly-CSharp
            var methods = assembly.GetTypes()
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                .Where(method => method.GetCustomAttributes<DebugKeyAttribute>().Any());

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<DebugKeyAttribute>();
                if (attribute != null)
                {
                    if (!_debugMethods.ContainsKey(attribute.Key))
                    {
                        _debugMethods[attribute.Key] = new List<MethodInfo>();
                    }
                    _debugMethods[attribute.Key].Add(method);
                }
            }
        }

        private void CheckAndInvoke(Key key)
        {
            if (_debugMethods.TryGetValue(key, out var methods))
            {
                foreach (var method in methods)
                {
                    // Проверяем, является ли метод статическим или требует экземпляра
                    if (method.IsStatic)
                    {
                        method.Invoke(null, null);
                    }
                    else
                    {
                        // Ищем все объекты в сцене, у которых есть данный метод
                        var instances = Object.FindObjectsByType(method.DeclaringType, FindObjectsSortMode.None);

                        foreach (var instance in instances)
                        {
                            method.Invoke(instance, null);
                        }                    }
                }
            }
        }

        public void Dispose()
        {
            _inputActions.Debug.Disable();
            _inputActions.Debug.DebugAction.performed -= ctx => CheckAndInvoke(Key.T);
            _inputActions.Dispose();
        }
    }
#endif
}