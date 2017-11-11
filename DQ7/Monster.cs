﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
	class Monster
	{
		private readonly uint mAddress;
		public String Name { get; set; }

		public Monster(uint address)
		{
			mAddress = address;
		}

		public uint Kill
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress, 2);
			}

			set
			{
				Util.WriteNumber(mAddress, 2, value, 0, 999);
			}
		}

		public uint Item
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress, 2);
			}

			set
			{
				Util.WriteNumber(mAddress, 2, value, 0, 999);
			}
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 6, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 6, 1, value, 0, 99);
			}
		}

		public uint Friend
		{
			get
			{
				return SaveData.Instance().ReadNumber(mAddress + 7, 1);
			}

			set
			{
				Util.WriteNumber(mAddress + 7, 1, value, 0, 99);
			}
		}
	}
}
