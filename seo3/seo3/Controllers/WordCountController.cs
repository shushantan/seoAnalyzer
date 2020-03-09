using seo3.Models;
using seo3.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace seo3.Controllers
{
	public class GetWordCountController : ApiController
	{
		WordCountResponse res = new WordCountResponse();
		List<string> stopWord = new List<string>() { "a", "the", "and", "or", "is", "by"};

		public GetWordCountController ()
		{
			
		}


		/// <summary>
		/// Get word count from input based on options selected
		///  
		/// </summary>
		/// <param name="input">Input can be either URL or Text</param>
		/// <param name="isFilterStopWord">set to true will filterout pre-defined stop word</param>
		/// <param name="options"> <list type="bullet"> <item> 0: calculates number of occurences of each word on the page </item> <item>  1: calculates number of occurences of each word on the page meta tags. </item>  <item> 2: calculates number of external links in the text.</item></list >  </param>
		/// <param name="sortByColumn"><para>1: sort by word </para><para> 1: sort by count</para></param>
		/// <param name="sortOrder"><para>0:ASC</para><para> 1: DESC</para></param>
		public WordCountResponse Get(string input, bool isFilterStopWord, OptionsEnum options,  int sortByColumn=2 , SortEnum sortOrder = SortEnum.DESC)
		{
			string content = ""; 
			Dictionary<string, int> result = new Dictionary<string, int>();
			Dictionary<string, int> resultSorted = new Dictionary<string, int>();
			if (!isFilterStopWord)
			{
				stopWord = new List<string>();
			}
			try
			{
				bool isUri = Uri.IsWellFormedUriString(input, UriKind.RelativeOrAbsolute);
				content = (isUri) ? WordCountUtils.LoadContentFromURL(input) : input;

				switch (options)
				{
					case OptionsEnum.COUNTLINKS:
						{
							result = WordCountUtils.GetLinkCount(content);
							break;
						}
					case OptionsEnum.COUNTWORDINMETA:
						{
							result = WordCountUtils.GetMetaTextCount(content, stopWord); 
							break;
						}
					case OptionsEnum.COUNTWORD:
					default:
						{
							result = WordCountUtils.GetWordCount(content, stopWord);
							break;
						}
				}

				resultSorted = WordCountUtils.SortResult(result, sortByColumn, sortOrder);
			}
			catch ( Exception ex)
			{
				//log it 

				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(ex.Message),
					ReasonPhrase = "Something went wrong"
				});

			}

			res.Source = input;
			res.IsFilterStopWord = isFilterStopWord;
			res.SelectedOption = options;
			res.result = resultSorted;

			return res;
		}



		

		 
	}
}
