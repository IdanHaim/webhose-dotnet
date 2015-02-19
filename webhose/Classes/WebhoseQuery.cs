using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace webhose
{

    //Query Object for be able to use advance search in you program
    public enum Languages
    {
        arabic, bulgarian, catalan, chinese, croatian, czech, danish, dutch, english, estonian, finnish, french, german, greek, hebrew, hungarian,
        icelandic, indonesian, italian, japanese, korean, latvian, lithuanian, norwegian, persian, polish, portuguese, romanian, russian, serbian, slovak, slovenian,
        spanish
        , swedish, turkish
    };

    public enum SiteTypes { Discussions, News, Blogs };

    public class WebhoseQuery
    {
		private List<string> allTerms;
        private List<string> someTerms;
        private String phrase;
        private String exclude;
		private List<SiteTypes> siteTypes;
        private List<Languages> languages;
        private List<string> sites;
        private String title;
		private String bodyText;

        public string Phrase
        {
            get {return this.phrase;}
            set {this.phrase = value;} 
        }

        public string Exclude
        {
            get { return this.exclude; }
            set { this.exclude = value; }
        }
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Body_Text
        {
            get { return this.bodyText; }
            set { this.bodyText = value; }
        }


        public void addAllTerms(params string[] terms) 
        {
            if (allTerms == null) 
            {
                allTerms = new List<string>();
            }
            allTerms.AddRange(terms);
        }

        public void addSomeTerms(params string[] terms)
        {
            if (someTerms == null)
            {
                someTerms = new List<string>();
            }
            someTerms.AddRange(terms);
        }

        public void addSiteTypes(params SiteTypes[] terms)
        {
            if (siteTypes == null)
            {
                siteTypes = new List<SiteTypes>();
            }
            siteTypes.AddRange(terms);
        }


        public void addLanguages(params Languages[] terms)
        {
            if (languages == null)
            {
                languages = new List<Languages>();
            }
            languages.AddRange(terms);
        }


        public void addSites(params string[] terms)
        {
            if (sites == null)
            {
                sites = new List<string>();
            }
            sites.AddRange(sites);
        }
        public WebhoseQuery()
        {
            this.allTerms = null;
            this.someTerms = null;
            this.phrase = null;
            this.exclude = null;
            this.siteTypes = null;
            this.languages = null;
            this.sites = null;
            this.title = null;
            this.bodyText = null;
        }

		public override String ToString() {
			List<string> terms = new List<string>();

			addTerm(terms, allTerms, "AND", null);
			addTerm(terms, someTerms, "OR", null);
			if (phrase != null) {
				terms.Add(@"""" + phrase + @"""");
			}
			if (exclude != null) {
				terms.Add("-(" + exclude + ")");
			}
			addTerm(terms, siteTypes, "OR", "siteType");
			addTerm(terms, languages, "OR", "language");
			addTerm(terms, sites, "OR", "site");
			if (title != null) {
				terms.Add("title:(" + title + ")");
			}
			if (bodyText != null) {
				terms.Add("text:(" + bodyText+ ")");
			}
			return String.Join(" AND ",terms.ToArray());
		}

		private void addTerm(List<String> terms, ICollection parts, String boolOp, String fieldName) {
			if(parts == null) return;

			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			Boolean first = true;
			foreach(Object part in parts) {
				if(first) {
					first = false;
				} 
				else
				{ 
					sb.Append(" ").Append(boolOp).Append(" ");
				}
				if(fieldName != null) 
				{
					sb.Append(fieldName).Append(":");
				}
				sb.Append(part);
			}
			sb.Append(")");
			terms.Add(sb.ToString());
		}
	}
}