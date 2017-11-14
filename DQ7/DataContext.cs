using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ7
{
	class DataContext
	{
		public Info Info { get; set; } = Info.Instance();
		public List<PassingSlateMonsterInfo> PassingSlateMonsters { get; set; } = new List<PassingSlateMonsterInfo>();
		public ObservableCollection<Charactor> Charactors { get; set; } = new ObservableCollection<Charactor>();
		public ObservableCollection<BagItem> Bag { get; set; } = new ObservableCollection<BagItem>();
		public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();
		public ObservableCollection<Monster> Monsters { get; set; } = new ObservableCollection<Monster>();
		public ObservableCollection<MonsterStamp> MonsterStamps { get; set; } = new ObservableCollection<MonsterStamp>();
		public ObservableCollection<PartyMember> Party { get; set; } = new ObservableCollection<PartyMember>();
		public ObservableCollection<PassingSlate> PassingSlates { get; set; } = new ObservableCollection<PassingSlate>();

		public DataContext()
		{
			for (uint i = 0; i < 6; i++)
			{
				Charactors.Add(new Charactor(0x0C80 + i * 0x01EC));
			}

			BagItem item = null;
			for (uint i = Util.BagItemCount - 1; (int)i >= 0; i--)
			{
				item = new BagItem(0x0544 + i * 2, 0x09D4 + i, item);
				item.PropertyChanged += Item_PropertyChanged;
				Bag.Insert(0, item);
			}

			foreach (var place in Info.Instance().Places)
			{
				Places.Add(new Place(place.Value) { Name = place.Name });
			}

			foreach (var monster in Info.Instance().Monsters)
			{
				if (monster.Value == 0) continue;
				Monsters.Add(new Monster(0x1854 + (monster.Value - 1) * 8) { Name = monster.Name });
			}

			foreach (var monster in Info.Instance().Monsters)
			{
				if (monster.Value == 0) continue;
				if (monster.Value > 282) break;
				PassingSlateMonsters.Add(new PassingSlateMonsterInfo() { ID = monster.Value, Name = monster.Name });
			}

			/*
			for (uint i = 0; i < Util.MonsterStampCount; i++)
			{
				MonsterStamps.Add(new MonsterStamp(0x2E06 + i * 2));
			}
			*/

			for (uint i = 0; i < Util.PartyMemberCount; i++)
			{
				Party.Add(new PartyMember(0x0510 + i * 4));
			}

			for (uint i = 0; i < Util.PassingSlateCount; i++)
			{
				uint address = 0x32D0 + i * 44;
				if (SaveData.Instance().ReadNumber(address, 1) == 0) break;
				PassingSlates.Add(new PassingSlate(address));
			}
		}

		private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "ID") return;
			BagItem item = sender as BagItem;
			if (item == null) return;
			if (item.ID != 0) return;
			BagItem next = item.Next;
			if (next == null || next.ID == 0) return;
			item.Count = next.Count;
			item.ID = next.ID;
			next.Count = 0;
			next.ID = 0;
		}

		public uint Gold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0528, 4);
			}

			set
			{
				Util.WriteNumber(0x0528, 4, value, 0, 9999999);
			}
		}

		public uint BattleCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x1844, 4);
			}

			set
			{
				Util.WriteNumber(0x1844, 4, value, 0, 9999999);
			}
		}

		public uint PlayTimeHour
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3250, 4) / 30 / 3600;
			}

			set
			{
				if (value < 0) value = 0;
				if (value > 999) value = 999;
				uint time = SaveData.Instance().ReadNumber(0x3250, 4);
				time %= (30 * 3600);
				SaveData.Instance().WriteNumber(0x3250, 4, value * 30 * 3600 + time);
			}
		}

		public uint PlayTimeMinute
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3250, 4) / 30 / 60 % 60;
			}

			set
			{
				if (value < 0) value = 0;
				if (value > 59) value = 59;
				uint time = SaveData.Instance().ReadNumber(0x3250, 4);
				time /= (30 * 3600);
				SaveData.Instance().WriteNumber(0x3250, 4, time * 30 * 3600 + value * 30 * 60);
			}
		}
	}
}
