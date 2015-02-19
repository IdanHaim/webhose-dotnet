
using Newtonsoft.Json.Linq;

namespace webhose
{
    public class webhosePost
    {
        public string url;
        public string title;
        public string author;
        public string text;
        public string published;
        public string crawled;
        public string ordInThread;
        public string languages;
        public ThreadToken thread;

        public webhosePost(JToken post)
        {
            url = (string)post["url"];
            title = (string)post["title"];
            author = (string)post["author"];
            text = (string)post["text"];
            published = (string)post["published"];
            crawled = (string)post["crawled"];
            ordInThread = (string)post["ord_in_thread"];
            languages = (string)post["language"];
            thread = new ThreadToken(post["thread"]);
        }

        public override string ToString()
        {
            string print = "url: " + url + "\n" +
            "title: " + title + "\n" +
            "author: " + author + "\n" +
            "text: " + text + "\n" +
            "published: " + published + "\n" +
            "crawled: " + crawled + "\n" +
            "languages: " + languages + "\n" +
            "ord_in_thread: " + ordInThread + "\n" +
            "thread:\n" +
            "{\n" + thread + "\n}";

            return print;
        }

    }

}
