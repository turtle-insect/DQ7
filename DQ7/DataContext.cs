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
		public List<MonsterInfo> PassingSlateMonsters { get; set; } = new List<MonsterInfo>();
		public List<MonsterInfo> StampMonsters { get; set; } = new List<MonsterInfo>();
		public ObservableCollection<Charactor> Charactors { get; set; } = new ObservableCollection<Charactor>();
		public ObservableCollection<BagItem> Bag { get; set; } = new ObservableCollection<BagItem>();
		public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();
		public ObservableCollection<TownMonster> TownMonsters { get; set; } = new ObservableCollection<TownMonster>();
		public ObservableCollection<MonsterBook> Monsters { get; set; } = new ObservableCollection<MonsterBook>();
		public ObservableCollection<MonsterStamp> MonsterStamps { get; set; } = new ObservableCollection<MonsterStamp>();
		public ObservableCollection<MonsterParkPlace> MonsterParkPlaces { get; set; } = new ObservableCollection<MonsterParkPlace>();
		public ObservableCollection<MonsterParkDormitory> MonsterParkDormitorys { get; set; } = new ObservableCollection<MonsterParkDormitory>();
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
				if (monster.ID != 0) Monsters.Add(new MonsterBook(0x1854 + (monster.ID - 1) * 8) { Name = monster.Name });
				if (monster.Passing) PassingSlateMonsters.Add(monster);
				if (monster.Stamp) StampMonsters.Add(monster);
			}

			for (uint i = 0; i < Util.MonsterStampCount; i++)
			{
				MonsterStamps.Add(new MonsterStamp(0x2E06 + i * 2));
			}

			foreach (var place in Info.Instance().ParkPlaces)
			{
				MonsterParkPlaces.Add(new MonsterParkPlace(0x00AC, place.Value) { Name = place.Name });
			}

			for (uint i = 0; i < Util.MonsterParkDormitoryCount; i++)
			{
				MonsterParkDormitorys.Add(new MonsterParkDormitory(0x04BC + i * 4, 0x3180 + i));
			}

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

			foreach (var monster in Info.Instance().TownMonsters)
			{
				TownMonsters.Add(new TownMonster(0x01E9, monster.Value) { Name = monster.Name });
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

		public uint Casino
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0530, 4);
			}

			set
			{
				Util.WriteNumber(0x0530, 4, value, 0, 9999999);
			}
		}

		public uint SmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0538, 4);
			}

			set
			{
				Util.WriteNumber(0x0538, 4, value, 0, 9999999);
			}
		}

		public uint MaxDamage
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x053C, 4);
			}

			set
			{
				Util.WriteNumber(0x053C, 4, value, 0, 9999999);
			}
		}

		public uint AllGold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x0540, 4);
			}

			set
			{
				Util.WriteNumber(0x0540, 4, value, 0, 9999999);
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

		public uint KillCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x1840, 4);
			}

			set
			{
				Util.WriteNumber(0x1840, 4, value, 0, 9999999);
			}
		}

		public uint WinCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x1848, 4);
			}

			set
			{
				Util.WriteNumber(0x1848, 4, value, 0, 9999999);
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

		public bool Proficiency
		{
			get
			{
				return SaveData.Instance().ReadBit(0x31A0, 1);
			}

			set
			{
				SaveData.Instance().WriteBit(0x31A0, 1, value);
			}
		}

		public uint PassingSlateCount
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3F14, 1);
			}

			set
			{
				Util.WriteNumber(0x3F14, 1, value, 0, 24);
			}
		}

		public bool PassingSlate
		{
			get
			{
				return SaveData.Instance().ReadBit(0x4198, 0);
			}

			set
			{
				SaveData.Instance().WriteBit(0x4198, 0, value);
			}
		}

		public bool MonsterTownInit
		{
			get
			{
				return SaveData.Instance().ReadBit(0x01E8, 4);
			}

			set
			{
				SaveData.Instance().WriteBit(0x01E8, 4, value);
			}
		}

		public bool MonsterTownLook
		{
			get
			{
				return SaveData.Instance().ReadBit(0x01E8, 5);
			}

			set
			{
				SaveData.Instance().WriteBit(0x01E8, 5, value);
			}
		}

		public bool MonsterTownStart
		{
			get
			{
				return SaveData.Instance().ReadBit(0x01E8, 6);
			}

			set
			{
				SaveData.Instance().WriteBit(0x01E8, 6, value);
			}
		}
	}
}
