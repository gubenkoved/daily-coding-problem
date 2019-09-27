<Query Kind="Program" />

// This problem was asked by Twitch.
// 
// Describe what happens when you type a URL into your
// browser and press Enter.

void Main()
{
	// 1. When domain name is enterred Domain Resolution happens which
	//    implies invokation of the DNS server which is registered in the network configuration
	//    if cached entry is not locally found; After this stage we know IP address we will be talking with;
	//
	// 2. Depending on the protocol being used different things might happen
	//    let's say for instance the protocol is HTTPS, then secure TLS conenction has to be established
	//    protocol also defines the defaul TCP port that will be used if it is not explictly state
	//    in case of HTTPS TCP connection is being openned (3 way handshake happens to do that)
	//    then TLS handhake follows which implies presenting and validation of the server certificate
	//    and agreement on the session secret;
	//
	// 3. In case of the HTTPS then HTTP GET request is being created and sent;
	//
	// 4. Browser receives the HTTP response and renders the page;
}
