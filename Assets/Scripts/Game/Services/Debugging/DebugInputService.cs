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
    /// DebugInputService работает только с Zenject, так как использует его апдейт систему.
    /// должен быть проинициализирован на уровне ProjectContext
    /// позволяет вызывать методы помеченые атрибутом [DebugKey(Key.S)] клавиша может быть любой.
    /// </summary>
    public class DebugInputService : IInitializable, ITickable, IDisposable
    {
        private readonly Dictionary<Key, List<MethodInfo>> _debugMethods = new();

        public void Initialize()
        {
            // Регистрируем методы с атрибутом DebugKeyAttribute
            RegisterDebugMethods();
        }

        private void RegisterDebugMethods()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");

            if (assembly == null)
            {
                Debug.LogWarning("Assembly-CSharp not found. Debug methods registration skipped.");
                return;
            }

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

        public void Tick()
        {
            // Проверяем нажатие каждой клавиши из зарегистрированных
            foreach (var key in _debugMethods.Keys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    Debug.Log($"Key pressed: {key}");
                    CheckAndInvoke(key);
                }
            }
        }

        private void CheckAndInvoke(Key key)
        {
            if (_debugMethods.TryGetValue(key, out var methods))
            {
                foreach (var method in methods)
                {
                    if (method.IsStatic)
                    {
                        method.Invoke(null, null);
                    }
                    else
                    {
                        var instances = Object.FindObjectsByType(method.DeclaringType, FindObjectsSortMode.None);
                        foreach (var instance in instances)
                        {
                            method.Invoke(instance, null);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            // Очищаем словарь при уничтожении
            _debugMethods.Clear();
        }
    }
#endif
}
