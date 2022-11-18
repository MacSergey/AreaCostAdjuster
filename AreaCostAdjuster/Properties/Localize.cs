namespace AreaCostAdjuster
{
	public class Localize
	{
		public static System.Globalization.CultureInfo Culture {get; set;}
		public static ModsCommon.LocalizeManager LocaleManager {get;} = new ModsCommon.LocalizeManager("Localize", typeof(Localize).Assembly);

		/// <summary>
		/// Adjust Pedestrian area maintenance cost 
		/// </summary>
		public static string Mod_Description => LocaleManager.GetString("Mod_Description", Culture);

		/// <summary>
		/// Pedestrian area cost multiplier (%)
		/// </summary>
		public static string PedestrianAreaMultiplier => LocaleManager.GetString("PedestrianAreaMultiplier", Culture);
	}
}