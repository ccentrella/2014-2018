namespace RecordPro
{
	internal class RPGrade
	{
		public string Name { get; private set; }
		public string Location { get; private set; }
		public RPGrade (string name, string location)
		{
			Name = name;
			Location = location;
		}
	}
}