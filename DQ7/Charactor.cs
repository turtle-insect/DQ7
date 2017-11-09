using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ7
{
	class Charactor
	{
		public ObservableCollection<CharactorItem> Items { get; set; } = new ObservableCollection<CharactorItem>();
		public ObservableCollection<CharactorOption> BattleMagics { get; set; } = new ObservableCollection<CharactorOption>();
		public ObservableCollection<CharactorOption> FieldMagics { get; set; } = new ObservableCollection<CharactorOption>();
		public ObservableCollection<CharactorOption> Skills { get; set; } = new ObservableCollection<CharactorOption>();
		public ObservableCollection<CharactorJob> Jobs { get; set; } = new ObservableCollection<CharactorJob>();

		private readonly uint mAddress;

		public Charactor(uint address)
		{
			mAddress = address;
			for (uint i = 0; i < 12; i++)
			{
				Items.Add(new CharactorItem(address + 0x0180 + i * 4));
			}

			foreach (var magic in Info.Instance().Magics)
			{
				BattleMagics.Add(new CharactorOption(address + 0x00E0, magic.Value) { Name = magic.Name });
				FieldMagics.Add(new CharactorOption(address + 0x0100, magic.Value) { Name = magic.Name });
			}

			foreach (var skill in Info.Instance().Skills)
			{
				Skills.Add(new CharactorOption(address + 0x00EA, skill.Value) { Name = skill.Name });
			}

			foreach (var job in Info.Instance().Jobs)
			{
				if (job.ID == 0) continue;
				Jobs.Add(new CharactorJob(address + job.Lv, address + job.Exp) { Name = job.Name });
			}
		}

		public String Name
		{
			get
			{
				return SaveData.Instance().ReadText(mAddress + 0x01B0, 12);
			}

			set
			{
				SaveData.Instance().WriteText(mAddress + 0x01B0, 12, value);
				SaveData.Instance().WriteText(mAddress + 0x01CA, 12, value);
			}
		}

		public uint Job
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0038, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mAddress + 0x0038, 1, value);
			}
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x002E, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x002E, 1, value, 1, 99);
			}
		}

		public uint Power
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0018, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0018, 2, value, 1, 999);
			}
		}

		public uint Speed
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0028, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0028, 2, value, 1, 999);
			}
		}

		public uint Defense
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x001A, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x001A, 2, value, 1, 999);
			}
		}

		public uint Intelligence
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x002A, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x002A, 2, value, 1, 999);
			}
		}

		public uint Cool
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x002C, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x002C, 2, value, 1, 999);
			}
		}

		public uint HP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x001C, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x001C, 2, value, 0, 999);
			}
		}

		public uint MaxHP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x001E, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x001E, 2, value, 1, 999);
				Util.WriteNumber(mAddress + 0x0020, 2, value, 1, 999);
			}
		}

		public uint MP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0022, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0022, 2, value, 0, 999);
			}
		}

		public uint MaxMP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0024, 2);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0024, 2, value, 1, 999);
				Util.WriteNumber(mAddress + 0x0026, 2, value, 1, 999);
			}
		}

		public uint Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 0x0014, 4);
			}

			set
			{
				Util.WriteNumber(mAddress + 0x0014, 4, value, 1, 9999999);
			}
		}
	}
}
