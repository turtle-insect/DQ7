using System;

namespace DQ7
{
	class MonsterInfo : ILineAnalysis
	{
		public uint ID { get; private set; }
		public String Name { get; private set; }
		public bool Passing { get; private set; }
		public bool Stamp { get; private set; }

		public bool Line(string[] oneLine)
		{
			ID = Convert.ToUInt32(oneLine[0]);
			Name = oneLine[1];
			if (oneLine.Length > 2) Passing = oneLine[2] == "0" ? false : true;
			if (oneLine.Length > 3) Stamp = oneLine[3] == "0" ? false : true;
			return true;
		}
	}
}
