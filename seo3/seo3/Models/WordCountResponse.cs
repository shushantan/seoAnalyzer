using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace seo3.Models
{
	public class WordCountResponse
	{
		//string URL, bool isFilterStopWord, bool countWordInMetaTag, bool countExternalList
		public string Source { get; set; }
		 
		public bool IsFilterStopWord { get; set; }

		public OptionsEnum SelectedOption { get; set; }

		public Dictionary<string, int> result = new Dictionary<string, int>();

	}
}