# Problem statement

This problem was asked by Google.

Design a system to crawl and copy all of Wikipedia using a distributed network of machines.

More specifically, suppose your server has access to a set of client machines. Your client machines can execute code you have written to access Wikipedia pages, download and parse their data, and write the results to a database.

Some questions you may want to consider as part of your solution are:

How will you reach as many pages as possible?
How can you keep track of pages that have already been visited?
How will you deal with your client machines being blacklisted?
How can you update your database when Wikipedia pages are added or updated?

# Design

The central piece of the crawler would be a crawling queue which can be hosted by variety of the exiting tools like Redis, Azure Queues, Azure Service Bus, etc.

The second major components is tracking of the already visited pages. For this purpose we can use scalable storage like Azure Tables which will support efficient queries.

We would also need a storage for the pages itself, here we can use various data storages depending on the usage: Elastic Search if we would need to full-text search what we have or Azure Blobs Storage if we simple need to have an archive by URLs.

Then the implementation of the workers and scaling out is trivial:

* Get the URL from the queue
* Check if the URL is not yet processed
* Crawl the URL, and add found URLs into the queue if not yet processed

We can also maintain a hash of the content on the page in order to detect if there was a change since the last pass.

In order to tackle possible blacklisting we need to limit amount of the requests we make from single unit (or IP address) -- so we would need to have a sufficiently large pool of the machines. We can also incorporate rate limiting into the workers itself (using something like "leaky bucket algorithm"). Also the machines themselves can use set of proxies in order to spread the load among many IP addresses.