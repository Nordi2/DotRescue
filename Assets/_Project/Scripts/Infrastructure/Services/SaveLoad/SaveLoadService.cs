using _Project.Scripts.Data;
using _Project.Scripts.Extension;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using DebugToolsPlus;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService :
        ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(
            IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                D.LogWarning(GetType().Name.ToUpper(), $"SAVE PROGRESS {progressWriter.GetType().Name}",
                    DColor.AQUAMARINE, true);

                progressWriter.UpdateProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            if (!PlayerPrefs.HasKey(ProgressKey))
                return null;

            D.LogWarning(GetType().Name.ToUpper(), "LOAD PROGRESS", DColor.AQUAMARINE);
            
            return PlayerPrefs.GetString(ProgressKey).ToDeserialized<PlayerProgress>();
        }
    }
}