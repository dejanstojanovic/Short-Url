[![Build status](https://ci.appveyor.com/api/projects/status/github/dejanstojanovic/ShortUrl?branch=master&svg=true)](https://ci.appveyor.com/project/dejanstojanovic/ShortUrl/branch/master)

# ShortUrl
.NET implementation of URL shortening service

##How to set it up

All you need is to build and publish WebAPI to your IIS host server and configure database connection string in web.config file. On the first run database will be automatically created.
```xml
<configuration>
  <connectionStrings>
    <add name="ShortUrl" connectionString="Server=<SQL-SERVER-INSTANCE>;Database=ShortUrl;User Id=<USERNAME>;Password=<PASSWORD>;" providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

##How to use

The service cosists of two basic functionalities:

####Create short url
Simple post of long URL to controller action "SHORT" will create record with short URL key and retrieve it back

```http
POST /Short/ HTTP/1.1
Host: localhost:37626
Content-Type: application/json
Cache-Control: no-cache

"http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api"
```

If the short URL already exists with the short URL key then existing short URL key is retrieved.

####Navigate to short url
Passing the short URL key to service in GET request will return redirection header values (including long URL string) order to navigate browser to the long URL assigned to the key.

```http
GET /1639fe HTTP/1.1
Host: localhost:37626
Content-Type: application/json
Cache-Control: no-cache
```
