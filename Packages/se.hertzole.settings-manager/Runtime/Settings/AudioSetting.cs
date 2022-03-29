﻿using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Hertzole.Settings
{
#if UNITY_EDITOR
	[CreateAssetMenu(fileName = "New Audio Setting", menuName = "Hertzole/Settings/Audio Setting")]
#endif
	public class AudioSetting : Setting<int>, IMinMaxInt, ICanHaveSlider
	{
		[SerializeField]
		private bool hasMinValue = true;
		[SerializeField]
		private int minValue = 0;
		[SerializeField]
		private bool hasMaxValue = true;
		[SerializeField]
		private int maxValue = 100;
		[SerializeField] 
		private bool enableSlider = true;
		[SerializeField]
		private AudioMixer targetAudioMixer = default;
		[SerializeField]
		private string targetProperty = default;

		public bool HasMinValue { get { return hasMinValue; } set { hasMinValue = value; } }
		public bool HasMaxValue { get { return hasMaxValue; } set { hasMaxValue = value; } }

		public int MinValue { get { return minValue; } set { minValue = value; } }
		public int MaxValue { get { return maxValue; } set { maxValue = value; } }
	
		public bool EnableSlider { get { return enableSlider; } set { enableSlider = value; } }
		public bool WholeSliderNumbers { get { return true; } }

		public AudioMixer TargetAudioMixer { get { return targetAudioMixer; } set { targetAudioMixer = value; } }
		public string TargetProperty { get { return targetProperty; } set { targetProperty = value; } }

		protected override void SetValue(int newValue)
		{
			if (hasMinValue && newValue < minValue)
			{
				newValue = minValue;
			}
			else if (hasMaxValue && newValue > maxValue)
			{
				newValue = maxValue;
			}

			if (newValue != value)
			{
				InvokeOnValueChanging(value);

				UpdateVolume(newValue);
				value = newValue;
				
				InvokeOnValueChanged(value);
				InvokeOnSettingChanged();
			}
		}

		protected override int TryConvertValue(object newValue)
		{
			return Convert.ToInt32(newValue);
		}

		public override void SetSerializedValue(object newValue)
		{
			base.SetSerializedValue(newValue);
			UpdateVolume(value);
		}

		private void UpdateVolume(int newValue)
		{
			if (targetAudioMixer != null)
			{
				float volume = newValue == 0 ? 0 : newValue / 100f;
				targetAudioMixer.SetFloat(targetProperty, volume <= 0 ? -80f : Mathf.Log10(volume) * 20);
			}
		}
	}
}