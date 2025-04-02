using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerFacade : MonoBehaviour, 
        IDieble
    {
        public Transform PlayerTransform => transform;
        
        public void Die()
        {
            Debug.Log("Die");
            gameObject.SetActive(false);
        }
    }
}