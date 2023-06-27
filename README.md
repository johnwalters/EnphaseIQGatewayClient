# Enphase IQ Gateway Client

This site provides the ability to talk to the Enphase IQ Gateway.

Enphase IQ Gateway is a box that solar panel systems use to monitor the generation and consumption of power.

You can submit requests to the IQ Gateway and receive JSON responses in a browser. For that, you'll need an authorization token. That is explained [here](https://enphase.com/download/accessing-iq-gateway-local-apis-or-local-ui-token-based-authentication).

What this web client does is allow you to make the same requests, and it displays the responses in a more readable, formatted way.

You'll still need to [obtain an auth token](https://enphase.com/download/accessing-iq-gateway-local-apis-or-local-ui-token-based-authentication), and set it in the appsettings.json configuration file at the root of the IQClientSite project.


You can get pretty much this same data online at [enlighten.enphaseenergy.com](https://enlighten.enphaseenergy.com/). The reason I wrote this is that often I find that the online service is showing me data from hours behind.

Still to come in future versions: Store the responses in a database, and a data collector that periodically submits requests and stores the responses. Also some views in the site that show historical data.

**Development**
- IDE: Visual Studio 2022
- Language: C# and TypeScript
- SDK: .NET 7
- Web: .NET Server, Angular Client


**Build**

Clone the repository recursively.
Open EnphaseIQGatewayClient.sln in VS2022 and build the solution.

