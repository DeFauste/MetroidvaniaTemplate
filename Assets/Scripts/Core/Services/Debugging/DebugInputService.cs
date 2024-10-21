using Debugging;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Services.Debugging
{
#if UNITY_EDITOR
    /// <summary>
    /// DebugInputService управляет вводом и использует DebugInputSystem для вызова методов.
    /// </summary>
    public class DebugInputService : ITickable
    {
        private DebugInputSystem _debugInputSystem;

        [Inject]
        private void Construct(DebugInputSystem debugInputSystem)
        {
            _debugInputSystem = debugInputSystem;
        }

        public void Tick()
        {
            foreach (Key key in _debugInputSystem.RegisteredKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    Debug.Log($"Key pressed: {key}");
                    _debugInputSystem.InvokeDebugMethod(key);
                }
            }
        }
    }
#endif
}
