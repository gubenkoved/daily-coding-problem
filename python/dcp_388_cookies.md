# Question

This problem was asked by Airtable.

How would you explain web cookies to someone non-technical?

# Answer

When you open a page via the *Browser* you are talking to some *Server*.
*Server* is a program that runs on some another computer somewhere and is able to understand the page you want to get,
and transfer it back to your *browser* to show it to you.

*Cookies* is some additional information that *server* might want to pass to your *browser* to temporary store. It's 
usually needed to identify you, or remember your specific preferences. For instance, when you log in into a system
you usually provide credentials. After credentials are checked server can give you temporary piece of information that
will prove that you already passed authentication procedure.

*Cookies* are sent to the *Server* every time you want to view a site that gave it to you. You are the owner of this
information, but it's not really for you -- it's for *Server*. Usually this piece of information is cryptographically
protected from tampering so that in the example of the authentication you can not simply pass along piece of information
that will impersonate you as someone else. 