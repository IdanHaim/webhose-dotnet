using System;
using System.Threading;
using System.Text;
using webhose;

namespace TestingWebhoseExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			//for better console output
//			Console.OutputEncoding = Encoding.UTF8;
//			Console.InputEncoding = Encoding.UTF8;

			WebhoseRequest clientRequest = new WebhoseRequest ("YOUR_APIKEY");
			WebhoseResponse response = clientRequest.getResponse ("aftonbladet");

			foreach (WebhosePost post in response.posts) {
				//Console.WriteLine (post);
			}
			Console.WriteLine ();

			//exapmle for query
			WebhoseQuery clientQuery = new WebhoseQuery();
	
			clientQuery.AddAllTerms ("apple"); // what you want to search
			clientQuery.AddSomeTerms ("apple iphone","samsung","esny"); // words that may be in the search
			clientQuery.AddLanguages (Languages.english);
			clientQuery.AddSiteSuffix("com"); // Limit the results to a specific site suffix
			//clientQuery.AddSites ("macnn.com", "appleinsider.com");
			//clientQuery.AddOrganization ("apple", "MNM Media");
			clientQuery.ResponseSize = 50;
			clientQuery.Author = "";
			clientQuery.AddCountries ("US"); // filtring by country  - give much less data - To get the full country code list, visit countrycode.org.

			Console.WriteLine ("responceWithQuery");
			WebhoseResponse responceWithQuery = clientRequest.getResponse (clientQuery);
			Console.WriteLine (responceWithQuery);

			Console.ReadKey ();

			//Just change NEW_API_KEY to your new API
			//clientRequest.setAPI("NEW_API_KEY");

			WebhoseResponse moreFromResponse = response.getNext ();
			Console.WriteLine (moreFromResponse.posts.Count);
			Console.WriteLine (response.posts [5].title);

			//for getting post every 5 minutes
			//			while(true) {
			//				try {
			//					foreach (webhosePost post in response.posts) 
			//					{
			//						//just change the performAction to what you want to do with your posts
			//						//performAction(post);
			//					}
			//					Thread.Sleep(300000);
			//					response = response.getNext();
			//
			//				} catch (ThreadInterruptedException e) {
			//					Console.WriteLine (e.Message);
			//					break;
			//				}
			//			}

			Console.WriteLine ("total:" +response.totalResults);
			Console.WriteLine ("response"+response.posts.Count);

		}
	}
}

