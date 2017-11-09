using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ7
{
    class CharactorJob
    {
		private readonly uint mLvAddress;
		private readonly uint mExpAddress;

		public String Name { get; set; }

		public CharactorJob(uint lv, uint exp)
		{
			mLvAddress = lv;
			mExpAddress = exp;
		}

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mLvAddress, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(mLvAddress, 1, value);
			}
		}

		public uint Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mExpAddress, 2);
			}

			set
			{
				Util.WriteNumber(mExpAddress, 2, value, 0, 999);
			}
		}
	}
}
