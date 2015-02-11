# webhose.io client for C#

A simple way to access the [webhose.io](https://webhose.io) API from your C# code


```C#
    webhoseRequest clientRequest = new webhoseRequest ("YOUR_API_KEY");
			webhoseResponse response = clientRequest.getResponse ("skyrim");

			foreach (webhosePost post in response.posts) {
				Console.WriteLine (post);
			}

			Console.WriteLine(response);
```

## API Key

To make use of the webhose.io API, you need to obtain a token that would be
used on every request. To obtain an API key, create an account at
https://webhose.io/auth/signup, and then go into
https://webhose.io/dashboard to see your token.

## Installing
Choose one from the options below:

1.You can find webhose in the NuGet add packages and install it to your project under Webhose.dotnet.
  see more in here http://docs.nuget.org/consume/installing-nuget.

2.Just Download the project and add it to your project References

3.From the Package Manager Console just run the following command Package Manager Console 
  Install-Package Webhose.dotnet

## Use the API

To get started you need to set your access token.
(Replace YOUR_API_KEY with your actual API key).

```C#
    webhoseRequest clientRequest = new webhoseRequest ("YOUR_API_KEY");
    //For you convenient if you want to use different API key to your request just do the following code
    clientRequest.setAPI("NEW_API_KEY");
```

Now you can make a request and inspect the results:

```C#
    WebhoseResponse response = clientRequest.getResponse("skyrim");
    Console.WriteLine(response.totalResults);
    // example output: 1047

    Console.WriteLine (response.posts.Count);
    // example output: 100

    Console.WriteLine (response.posts [0].languages);
    // example output: english

    Console.WriteLine (response.posts [5].title)
    // example output: 【其他】Vilja-Ver4.01-繁體ESP檔完整版
```

If there are more than one page of results, use the `getMore()` method to
fetch the next page.

```C#
    webhoseResponse moreFromResponse = response.getNext ();
    Console.WriteLine (moreFromResponse.posts.Count);)
    // Will output 100 in this case, or 0 in case there are not any posts  
```

The ``getMore()`` method is also useful for fetching new results that were
discovered since the last request.

```C#
    webhoseRequest clientRequest = new webhoseRequest (YOUR_API_KEY);
    WebhoseResponse response = clientRequest.getResponse("minecraft");
    // Fetch new results every 5 minutes
   	while(true){
	    try {
		foreach (webhosePost post in response.posts) 
		{
			//just change the performAction to what you want to do with your posts
			//performAction(post);
		}
		Thread.Sleep(300000);
		response = response.getNext();

	    } catch (ThreadInterruptedException e) {
		Console.WriteLine (e.Message);
		break;
	    }
	}
```

## Full documentation

### webhoseRequest class

* webhoseRequest(token)

  * token - your API key

* search(query)

  * query - the search query, either as a search string, or as a Query object

### webhoseQuery class

webhoseQuery objects correspond to the advanced search options that appear on https://webhose.io/use

webhoseQuery objects have the following members:

* ``allTerms`` - a list of strings, all of which must appear in the results
* ``someTerms`` - a list of strings, some of which must appear in the results
* ``phrase`` - a phrase that must appear verbatim in the results
* ``exclude`` - terms that should not appear in the results
* ``siteType`` - one or more of ``discussions``, ``news``, ``blogs``
* ``language`` - one or more of language names, in lowercase english
* ``site`` - one or more of site names, top level only (i.e., yahoo.com and not news.yahoo.com)
* ``title`` - terms that must appear in the title
* ``bodyText`` - term that must appear in the body text

webhoseQuery objects implement the ``toString()`` method, which shows the resulting search string.
to use the webhoseQuery simply create new webhoseQuery put all the parameters that you want to look for and make the webhoseResponse
```C#
     webhoseQuery clientQuery = new webhoseQuery();
     clientQuery.add_AllTerms ("skyrim","world");
     clientQuery.add_languages (Languages.english, Languages.hebrew);
     clientQuery.Phrase = "level";
     
     //Getting response with Query
     webhoseResponse responceWithQuery = clientRequest.getResponse (clientQuery);
     	    	
```
### WebhoseResponse class

Response objects have the following members:

* ``totalResults`` - the total number of posts which match this search
* ``moreResultsAvailable`` - the number of posts not included in this response
* ``posts`` - a list os Post objects
* ``next`` - a URL for the next results page for this search

Response objects implement the ``__iter__()`` method, which can be used to loop
over all posts matching the query. (Automatic page fetching)

### WebhosePost and WebhoseThread classes

WebhosePost and WebhoseThread object contain the actual data returned from the
API. Consult https://webhose.io/documentation to find out about their structure.

### More things you should know

Inside program you can find some Samples how to use the webhose

