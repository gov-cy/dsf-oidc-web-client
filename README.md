### <img src=https://github.com/gov-cy/govdesign/blob/main/oidc-logo.png height=75> 

# DSF OIDC Web App Client Demo
This is a demo application to demonstrate how the OpenID Connect code flow is implemented using a mock identity server.
### <img src=https://github.com/gov-cy/govdesign/blob/main/oidc.png height=250> 

## Features
This web client is a demo implementation that contains the following:
* OIDC Configuration using a mock identity server [dsf-idsrv-dev]
* Display id and access tokens retrieved from the IdentityServer for testing an API accepting the access token
* Call a sample Authorized API endpoint to test authorization  [git-api-template]
* more to come ...

## Integrations

### Mock Identity Server
The project also simulates a web client that connects to an Identity Server using OpenId Connect (OIDC)

## How it Works
### Installation
```
git clone https://github.com/gov-cy/dsf-identity-web-client-demo.git
cd src\dsf-identity-web-client-demo
dotnet build
dotnet run
```

### Configuration (appsettings.json)
All configuration settings can be set in the appsettings.json file

**Identity server used to validate the jwt tokens**
```
"IdentityServer": {
    "Authority": "https://dsf-idsrv-dev.dmrid.gov.cy"
}
```

**Call Mock API**
```
"ApiClientKey": "12345678901234567890123456789000",

"ApiEndpoints": {
    "GetTodoItems": "https://dsf-api-test.dmrid.gov.cy/api/v1/TodoItems"
}
```

Also, the OIDC settings provided by the Identity Authority should be set in Program.cs
```
options.ClientId = "<your-client-id>";
options.ClientSecret = "<your-secret>";
```

## Tech
* .NET6 C#
* Oidc Authentication (Mock CyLogin) [dsf-idsrv-dev]
* Razor Pages

## License

MIT

**Free Software, Hell Yeah!**

#### Non-production Usage. This Software is provided for evaluation, testing, demonstration and other similar purposes. Any license and rights granted hereunder are valid only for such limited non-production purposes.

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job.)

   [dsf-idsrv-dev]: <https://dsf-idsrv-dev.dmrid.gov.cy>   
   [git-repo-url]: <https://github.com/gov-cy/dsf-identity-web-client-demo>
   [dsf-api-standard]: <https://dsf.dmrid.gov.cy/2022/05/17/technical-policy/>
   [git-api-template]: <https://github.com/gov-cy/dsf-api-template-net6.git>
   [http-status-codes]: <https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-7.0>
