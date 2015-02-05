using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace webhose
{ 
    public class webhoseResponse
    {
        readonly JObject jsonfile;
        public List<webhosePost> posts;
        public int totalResults;
		public string next;
		public int left;
		public int moreResultsAvailable;
        public webhoseResponse(String query, String url , String token)
        {
            string headers = "/search?token=" + token + "&q=" + query;
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url + headers);
                jsonfile = (JObject)JsonConvert.DeserializeObject(json);
            }

            totalResults = (int)jsonfile["totalResults"];
            next = url+jsonfile["next"].ToString();
            left = (int)jsonfile["requestsLeft"];
			moreResultsAvailable = (int)jsonfile["moreResultsAvailable"];
			posts = retrievePosts(jsonfile);
        }

        public webhoseResponse(String url) 
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                jsonfile = (JObject)JsonConvert.DeserializeObject(json);
            }

            totalResults = (int)jsonfile["totalResults"];
            next = url + jsonfile["next"].ToString();
			left = (int)jsonfile["requestsLeft"];
			moreResultsAvailable = (int)jsonfile["moreResultsAvailable"];
			posts = retrievePosts(jsonfile);
        }

        public webhoseResponse getNext()
        {
            if (next.Equals(""))
            {
                return null;
            }
            else
            {
                return new webhoseResponse(next);
            }
        }
     
        public override string ToString()
        {
            StringBuilder responseString = new StringBuilder();
            foreach (webhosePost post in posts)
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

		private List<webhosePost> retrievePosts(JObject json)
        {
            List<webhosePost> postsList = new List<webhosePost>();
            foreach (JToken post in json["posts"])
	        {
				postsList.Add(new webhosePost(post));
	        }
			return postsList;
        }
       
    }
}
