	using System;
	using System.Collections.Generic;
	using System.Text;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	namespace webhose
	{ 
	    public class WebhoseResponse
	    {
	        readonly JObject jsonfile;
	        public List<WebhosePost> posts;
	        public int totalResults;
		public string next;
		public int left;
		public int moreResultsAvailable;
	        public WebhoseResponse(String query, String url , String token)
	        {
	            string headers = "/search?token=" + token + "&q=" + query;
	            using (var webClient = new System.Net.WebClient())
	            {	
			webClient.Encoding = Encoding.UTF8;
	                var json = webClient.DownloadString(url + headers);
			jsonfile = (JObject)JsonConvert.DeserializeObject(json);
	                
	            }

	            totalResults = (int)jsonfile["totalResults"];
		    next = url + jsonfile ["next"];
	            left = (int)jsonfile["requestsLeft"];
		    moreResultsAvailable = (int)jsonfile["moreResultsAvailable"];
		    posts = retrievePosts(jsonfile);
	        }

	        public WebhoseResponse(String url) 
	        {
	            using (var webClient = new System.Net.WebClient())
	            {
			webClient.Encoding = Encoding.UTF8;
                	var json = webClient.DownloadString(url);
	                jsonfile = (JObject)JsonConvert.DeserializeObject(json);
	            }

	            totalResults = (int)jsonfile["totalResults"];
	            next = url + jsonfile["next"];
		    left = (int)jsonfile["requestsLeft"];
		    moreResultsAvailable = (int)jsonfile["moreResultsAvailable"];
		    posts = retrievePosts(jsonfile);
	        }

	        public WebhoseResponse getNext()
	        {
	            if (next.Equals(""))
	            {
	                return null;
	            }
	            else
	            {
	                return new WebhoseResponse(next);
	            }
	        }
	     
	        public override string ToString()
	        {
	            StringBuilder responseString = new StringBuilder();
	            foreach (WebhosePost post in posts)
	            {
	                responseString.Append(post.ToString());
	                responseString.Append("\n\n");
	            }

	            responseString.Append("More Inforamtion:\n" +
	                "total_results: " + totalResults + "\n" +
	                "next: " + next + "\n" +
					"results_left: " + left + "\n" +
					"moreResultsAvailable: " + moreResultsAvailable + "\n");
	            return responseString.ToString();
	        }

			private List<WebhosePost> retrievePosts(JObject json)
	        {
	            List<WebhosePost> postsList = new List<WebhosePost>();
	            foreach (JToken post in json["posts"])
		        {
				postsList.Add(new WebhosePost(post));
		        }
			return postsList;
	        }
	       
	    }
	}
