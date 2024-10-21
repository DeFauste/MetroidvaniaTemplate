using System;
using UnityEngine.InputSystem;

namespace Debugging
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DebugKeyAttribute : Attribute
    {
        public Key Key { get; }

        public DebugKeyAttribute(Key key)
        {
            Key = key;
        }
    }
}
