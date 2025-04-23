using NinjaFSM.Game;
using NinjaFSM.Game.Character;
using NinjaFSM.UI.Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NinjaFSM.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ButtonController restartButton;
        [SerializeField] private NinjaController player;
        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI enemyKilledText;
        private void Awake()
        {
            restartButton.ButtonUp += Restart;
            player.CurrentHealthChanged += OnHealthChanged;
            EnemySpawner.EnemyKilled += OnEnemyKilledChanged;
        }

        private void OnDestroy()
        {
            restartButton.ButtonUp -= Restart;
            player.CurrentHealthChanged -= OnHealthChanged;
            EnemySpawner.EnemyKilled -= OnEnemyKilledChanged;
        }

        private void Restart()
        {
            Transition.FadeTo(RestartCurrentScene);

            void RestartCurrentScene()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void OnHealthChanged(int health)
        {
            healthBar.value = health;
        }

        private void OnEnemyKilledChanged(int totalEnemy)
        {
            enemyKilledText.text = $"Enemy killed: {totalEnemy}";
        }
    }
}