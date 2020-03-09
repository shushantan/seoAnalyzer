# SEO Analyzer

I have implement the Web API which are able performs a simple SEO analysis of the text. User
submits a text in English or URL, page filters out stop-words (e.g. ‘or’, ‘and’, ‘a’, ‘the’ etc), calculates number of
occurrences on the page of each word, number of occurrences on the page of each word listed in meta tags,
number of external links in the text.

## Requirement that are not fulfilled

UI.
I apologize for not having enough time to craete any UI for this project.
Hence, POSTMAN is required to view the result.

 
### Prerequisites

* Visual Studio
* POSTMAN 

### How to use the web api

__STEP 1__
Build the project and run "start without debugging
it should be hosted by iisexpress under http://localhost:5950 

__STEP 2__
open postman and install the  SiteCoreTest.postman_collection 
 
 
## Web API Param 


* __input Input can be either URL or any Text__
  * eg. &input=http://google.com 
 
* __isFilterStopWord__ 
  * true:  filter out hardcoded stop word.
  * false: Do not filter hardcoded stop words
  
* __options__ 
  * 0: calculates number of occurences of each word on the page  
  * 1: calculates number of occurences of each word on the page meta tags.   
  * 2: calculates number of external links in the text. 
* __sortByColumn__ 
  * 1: sort by word 
  * 2: sort by count
* __sortOrder__ 
  * 0:ASC 
  * 1: DESC

 
