using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.Debugging
{
    public class DebuggingTestScript : MonoBehaviour
    {
        [DebugKey(Key.S)]
        public void Test()
        {
            Debug.Log("Test");
        }
    }
}