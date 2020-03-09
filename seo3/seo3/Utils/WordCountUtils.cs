using seo3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace seo3.Utils
{
	static class WordCountUtils
	{
		private static readonly char[] _seperators = new char[] { ' ', '.', ',', ':', ';', '\t', '\n', '&', '?', '!', '"' };
		private static bool filterWord(string word, List<string> stopWords) =>
			!string.IsNullOrEmpty(word)
			&& !string.IsNullOrWhiteSpace(word)
			&& (stopWords != null && !stopWords.Contains(word.ToLower()))
			&& word.Length > 1
			&& Regex.IsMatch(word, "^[a-zA-Z]*$");
		public static Dictionary<string, int> GetWordCount(string text, List<string> stopWords)
		{
			var wordCountDictionary = new Dictionary<string, int>();
			if (string.IsNullOrEmpty(text) && string.IsNullOrWhiteSpace(text))
				return wordCountDictionary;
				var words = text.Trim()
					.Split(_seperators)
					.Where(w => filterWord(w, stopWords))
					.Select(w => w.ToLower())
					.ToList();

			foreach (var word in words)
			{ 
					if (wordCountDictionary.ContainsKey(word))
					{
						wordCountDictionary[word] += 1;
					}
					else
					{
						wordCountDictionary.Add(word, 1);
					} 
			}
			
			return wordCountDictionary;
		}

		public static Dictionary<string, int> GetLinkCount(string text)
		{
			var wordCountDictionary = new Dictionary<string, int>();
			if (string.IsNullOrEmpty(text) && string.IsNullOrWhiteSpace(text))
				return wordCountDictionary; 

			Regex ItemRegex = new Regex(@"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1", RegexOptions.Compiled);
			foreach (Match ItemMatch in ItemRegex.Matches(text))
			{
				string link = ItemMatch.Groups[2].ToString(); 
				if (wordCountDictionary.ContainsKey(link))
				{
					wordCountDictionary[link] += 1;
				}
				else
				{
					wordCountDictionary.Add(link, 1);
				}
			}  
			return wordCountDictionary;
		}

		public static Dictionary<string, int> GetMetaTextCount(string text, List<string> stopWords)
		{
			var wordCountDictionary = new Dictionary<string, int>();
			if (string.IsNullOrEmpty(text) && string.IsNullOrWhiteSpace(text))
				return wordCountDictionary;

			Regex ItemRegex = new Regex(@"<\s*meta\s+(?:[^>]*?\s+)?content=([""'])(.*?)\1", RegexOptions.Compiled);
			foreach (Match ItemMatch in ItemRegex.Matches(text))
			{
				string MetaContent = ItemMatch.Groups[2].ToString();
				var words = MetaContent.Trim()
					.Split(_seperators)
					.Where(w => filterWord(w, stopWords))
					.Select(w => w.ToLower())
					.ToList();

				foreach (var word in words)
				{
					if (wordCountDictionary.ContainsKey(word))
					{
						wordCountDictionary[word] += 1;
					}
					else
					{
						wordCountDictionary.Add(word, 1);
					}
				}

			}
			return wordCountDictionary;
		}

		public static Dictionary<string, int> SortResult(Dictionary<string, int> result, int sortByColumn, SortEnum sortOrder)
		{ 
		   
		   //Refactor to use func
			if (sortByColumn==1 )
			{
				if (sortOrder == SortEnum.ASC)
				{
					return (Dictionary<string, int>)result.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kpv => kpv.Value);
				}
				else
				{
					return (Dictionary<string, int>)result.OrderByDescending(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kpv => kpv.Value);

				}
			}
			else 
			{
				if (sortOrder == SortEnum.ASC)
				{
					return  result.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kpv=> kpv.Value); 
				}
				else
				{
					return ( Dictionary<string,int> ) result.OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kpv => kpv.Value);

				}
			}
			 
		}

		public static string LoadContentFromURL(string urlAddress)
		{
			string content = "";
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				if (response.StatusCode == HttpStatusCode.OK)
				{
					Stream receiveStream = response.GetResponseStream();
					StreamReader readStream = null;

					if (String.IsNullOrWhiteSpace(response.CharacterSet))
						readStream = new StreamReader(receiveStream);
					else
						readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

					content = readStream.ReadToEnd();

					response.Close();
					readStream.Close();
				}  
			}
			catch (Exception e)
			{
				throw e; 
			}

			return content;
		}
	}
}