namespace EduConnect.Application.Commons.Extensions
{
	public static class Utils
	{
		public static DateTime FixWronglyParsedDate(DateTime input)
		{
			// Example: input = 2025/12/01 → interpreted as 2025 (year), 12 (day), 01 (month)
			// We want to correct it to 2025/01/12
			return new DateTime(input.Year, input.Day, input.Month);
		}
	}
}
