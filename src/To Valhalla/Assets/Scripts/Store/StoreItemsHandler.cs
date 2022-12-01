using System.Collections.Generic;
using System.Linq;
using Artifacts;
using Hammers;
using NaughtyAttributes;
using Player;
using Services;
using UnityEngine;

namespace Store
{
    public class StoreItemsHandler : BaseGameHandler<StoreItemsHandler>
    {
        [SerializeField] private List<ScriptableHammerData> _hammers;
        [SerializeField] private List<ScriptableArtifactData> _artifacts;
        [SerializeField] private List<ScriptableSkinData> _skins;
        
        public List<IStoreItem> GetHammers() => _hammers.Select(hammer => hammer as IStoreItem).ToList();
        public List<IStoreItem> GetArtifacts() => _artifacts.Select(artifact => artifact as IStoreItem).ToList();
        public List<IStoreItem> GetSkins() => _skins.Select(skin => skin as IStoreItem).ToList();
        
        public int GetBoughtHammersCount() => _hammers.Count(hammer => hammer.IsBought());
        public int GetBoughtArtifactsCount() => _artifacts.Count(artifact => artifact.IsBought());
        public int GetBoughtSkinsCount() => _skins.Count(skin => skin.IsBought());

        public IStoreItem GetHammerByName(string hammerName) => _hammers.Find(hammer => hammer.GetName() == hammerName);
        public IStoreItem GetArtifactByName(string artifactName) => _artifacts.Find(artifact => artifact.GetName() == artifactName);
        public IStoreItem GetSkinByName(string skinName) => _skins.Find(skin => skin.GetName() == skinName);

        [Button]
        public void ResetAllItem()
        {
            _hammers.ForEach(hammer => hammer.Reset());
            _artifacts.ForEach(artifact => artifact.Reset());
            _skins.ForEach(skin => skin.Reset());
        }        
    }
}