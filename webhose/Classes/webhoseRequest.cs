using System;
using System.IO;

namespace webhose
{
    public class webhoseRequest
    {
        private string token;
		private webhoseResponse response;
        const string url = "https://webhose.io";
        public webhoseRequest(string token)
        {
            this.token = token;
        }
			

        public void config(string token)
        {
            this.token = token;
        }

        public void setAPI(string token)
        {
            this.token = token;
        }

        public string getAPI()
        {
            return token;
        }

        


		public webhoseResponse getResponse(string query)
        {
			try
			{
				response = new webhoseResponse(query, url, token);
				return response;
			}
			catch(System.Net.WebException)
			{
				Console.WriteLine ("Something went Wrong with the request please check you API key or you query");
				throw new System.IO.IOException();
			}
            
        }

		public webhoseResponse getResponse(webhoseQuery query)
        {
            string stringQuary = query.ToString();
            return this.getResponse(stringQuary);
        }

    }
}
