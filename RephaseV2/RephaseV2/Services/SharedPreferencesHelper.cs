using Android.App;
using Android.Content;

namespace RephaseV2.Services
{
	class SharedPreferencesHelper
	{
		/// <summary>
		/// Store a application preference in SharedPreferences
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void PutPreference(string key, dynamic value)
		{
			ISharedPreferences prefs = Application.Context.GetSharedPreferences("APP_SETTINGS", FileCreationMode.Private);

			ISharedPreferencesEditor editor = prefs.Edit();

			if (value is int)
			{
				editor.PutInt(key, value);
			}
			else if (value is string)
			{
				editor.PutString(key, value);
			}
			else if (value is long)
			{
				editor.PutLong(key, value);
			}
			else if (value is bool)
			{
				editor.PutBoolean(key, value);
			}
			else if (value is float)
			{
				editor.PutFloat(key, value);
			}

			editor.Apply();
		}

		/// <summary>
		///  Gets a application preference in SharedPreferences
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static dynamic GetPreference<T>(string key)
		{
			ISharedPreferences prefs = Application.Context.GetSharedPreferences("APP_SETTINGS", FileCreationMode.Private);

			if (typeof(T) == typeof(int))
			{
				return prefs.GetInt(key, 0);
			}
			if (typeof(T) == typeof(string))
			{
				return prefs.GetString(key, null);
			}
			if (typeof(T) == typeof(long))
			{
				return prefs.GetLong(key, 0);
			}
			if (typeof(T) == typeof(bool))
			{
				return prefs.GetBoolean(key, false);
			}
			if (typeof(T) == typeof(float))
			{
				return prefs.GetFloat(key, 0);
			}

			return null;
		}
	}
}