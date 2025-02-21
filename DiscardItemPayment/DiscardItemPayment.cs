using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace DiscardItemPayment
{
    public class DiscardItemPayment : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnItemsDiscardedByPlayerEvent.AddNonSerializedListener(this, DiacardItem);
        }

        private void DiacardItem(ItemRoster roster)
        {
            gold = 0;
            int totalNum = 0;
            float Multiplier = (float)GlobalSettings<MCMSettings>.Instance.DiscardItemPriceMultiplier;
            for (int i = 0; i < roster.Count; ++i)
            {
                //string Id = roster[i].EquipmentElement.Item.StringId;
                int value = roster[i].EquipmentElement.ItemValue;
                float priceMultiplier = 1.0f;
                if (roster[i].EquipmentElement.ItemModifier != null)
                { 
                    priceMultiplier = roster[i].EquipmentElement.ItemModifier.PriceMultiplier; 
                }
                
                //InformationManager.DisplayMessage(new InformationMessage($"乘数: {priceMultiplier}\n"));
                //InformationManager.DisplayMessage(new InformationMessage($"价值: {value}\n"));
                totalNum += roster[i].Amount;
                gold += (int)(value * roster[i].Amount * Multiplier);
            }
            Hero.MainHero.ChangeHeroGold(gold);
            if (totalNum > 0)
            {
                InformationManager.DisplayMessage(new InformationMessage($"丢弃了{totalNum}件物品，获得了{gold}第纳尔", new Color(1f, 0f, 0f, 1f)));
            }
        }

        private int gold = 0;

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("gold", ref gold);
        }
    }
}
