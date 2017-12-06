using System;
using System.Globalization;
using System.Windows.Data;

namespace DQ7
{
	class MonsterStampConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint id = (uint)value;
			uint index = 0;
			for(uint i = 0; i < Info.Instance().Monsters.Count; i++)
			{
				MonsterInfo info = Info.Instance().Monsters[(int)i];
				if (!info.Stamp) continue;
				if (id == info.ID) return index;
				index++;
			}
			return -1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int index = (int)value;
			int count = 0;
			for (uint i = 0; i < Info.Instance().Monsters.Count; i++)
			{
				MonsterInfo info = Info.Instance().Monsters[(int)i];
				if (!info.Stamp) continue;
				if (index == count) return info.ID;
				count++;
			}
			return 0;
		}
	}
}
