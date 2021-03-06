using System;
namespace Inclub.Api.Application.ViewModels
{
	public class ListItemDto
	{
		public int Value { get; set; }
		public string Text { get; set; }
	}

	public class ListItemStringDto
	{
		public string Value { get; set; }
		public string Text { get; set; }
	}

	public class ListItemGuidDto
	{
		public Guid Value { get; set; }
		public string Text { get; set; }
	}

	public class ListItemPrimeDto
	{
		public int Value { get; set; }
		public string Label { get; set; }
	}

	public class ListItemPrimeStringDto
	{
		public string Value { get; set; }
		public string Label { get; set; }
	}
}
