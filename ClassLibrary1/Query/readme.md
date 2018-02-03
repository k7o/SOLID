Classes in this folder don't use namespaces but static classes instead of identifying objects with intellisense (+ extension benefits)

Got the idea from:

http://scrapbook.qujck.com/managing-command-and-query-parameter-objects/

Discoverable

Parameter objects should be discoverable: we should do all we can to make our commands and queries easy to find.
By default our parameters will be mixed up amongst all the other classes available from the namspaces we are using. You need to know the first character or two of the parameter you are searching for before you can spot it amongst the intellisense noise.
A simple way to organise parameter objects is by their functional type (e.g. Command or Query).

Reducing code

We should do all we can to reduce the amount of typing required for the consumer of the API. This means more code for us up front but again I feel it is ultimately worth it.




Idea for query/handler design:

https://cuttingedge.it/blogs/steven/pivot/entry.php?id=92