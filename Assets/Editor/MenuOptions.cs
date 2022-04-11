using Character.Player;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class MenuOptions : MonoBehaviour
    {
       
        [MenuItem("Stuart/Player/Revive")]

        static void RevivePlayer()
        {
            var stats = FindObjectOfType<PlayerStats>();
            if (stats == null) return;

            stats.Revive();
        }
    }
}
