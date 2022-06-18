using System.Collections.Generic;
using UnityEngine;

namespace Hertzole.SettingsManager
{
	public interface IDropdownValues
	{
		void SetDropdownValue(int index);

		int GetDropdownValue();
		
		IReadOnlyList<(string text, Sprite icon)> GetDropdownValues();
	}
}