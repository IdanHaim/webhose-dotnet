using System;
using webhose;
using System.Threading;

namespace program
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			webhoseRequest clientRequest = new webhoseRequest ("YOUR_API_KEY");
			webhoseResponse response = clientRequest.getResponse ("skyrim");

			foreach (webhosePost post in response.posts) {
				Console.WriteLine (post);
			}
			Console.WriteLine ();

			//exapmle for query
			webhoseQuery clientQuery = new webhoseQuery();
			clientQuery.add_AllTerms ("skyrim","world");
			clientQuery.add_languages (Languages.english, Languages.hebrew);
			clientQuery.Phrase = "level";
			Console.WriteLine ("responceWithQuery");
			webhoseResponse responceWithQuery = clientRequest.getResponse (clientQuery);
			Console.WriteLine (responceWithQuery);

			Console.ReadKey ();

			//Just change NEW_API_KEY to your new API
			//clientRequest.setAPI("NEW_API_KEY");

			webhoseResponse moreFromResponse = response.getNext ();
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

