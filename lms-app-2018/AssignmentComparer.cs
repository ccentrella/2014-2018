using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public class AssignmentComparer : IEqualityComparer<Assignment>
	{
		public bool Equals(Assignment x, Assignment y)
		{
			// Ensure we're not dealing with null values
			if (x == null || y == null)
            {
                return false;
            }

            // Check each property to ensure the assignments match
            foreach (var prop in x.GetType().GetProperties())
			{
				object value = prop.GetValue(x);
				object otherValue = prop.GetValue(y);
				if (otherValue == null && value == null)
                {
                    continue;
                }
                else if ((otherValue == null && value != null) || (value == null && otherValue != null))
                {
                    return false;
                }
                else if (otherValue.ToString() != value.ToString())
                {
                    return false;
                }
            }

			// If we make to it this point, then all properties match
			return true;
		}

		public int GetHashCode(Assignment obj)
		{
			return base.GetHashCode();
		}
	}
}
