# SmsProject
1. Used Technologies and Techniques
  - Microsoft .NET Framework 4.5
  - IIS 7.5
  - Microsoft Visual Studio 2013 Ultimate
  - MsSQL
  - Web API
  - NHibernate/FluentNHiberante
  - Log4Net
  - Swagger
  - Code First
  - Generic Repository Pattern combined with Unit of Work Pattern
  - Task Parallel Library (TPL)
  - Factory Pattern
  - Test Driven Development (nunit)
  - Interceptor/MessageHandler
  - Global Error Handler & ExceptionLogger
  - Dynamic (weakly referenced variable identifier)

Note: I used MsSQL with code first, the good thing about nhibernate is database support. If you change connection string in cfg.xml to another database(includin mysql,oracle etc), it will all be fine. Only editing of GetStatistics query require a change according to MySQL. I used query in there to show “dynamic” usage, as intended.

2. Layers

SmsProject.App.API: contains configurations in startup.cs and swagger for easy documentation.
SmsProject.App.API.Common: contains extensibilities like global error handler and interceptor.
SmsProject.App.API.Core: contains controllers, used TPL for scalability and non-blocking codes.
SmsProject.App.Common: contains CallerInfo which stays in OwinContext and can be reached directly in all layers. CallerInfo is set in message handler(base).
SmsProject.App.Common.Logging: Generic logging dll that I coded a while ago.
SmsProject.App.Data.DAL: Generic repository and unit of work definitions are in here. DALExtensions.cs is to use “dynamic”.
SmsProject.App.Data.Persistence: PersistenceFactory required by NHibernate.
SmsProject.App.Model:Models and mappings are in here
SmsProject.App.Operation: contains business logic.
SmsProject.App.Operation.UnitTest: contains unit tests of operation layer and business logic.
SmsProject.App.SmsProvider: is to send SMS in reality. Right now, it is a dummy assembly. When somebody wants to code “actual send”, other assemblies won’t be affected.

Note: Developers can extend operation and controller(API.Core) layer as they wish. The other parts of architecture won’t grow horizontally and functionalities (like interceptorsetc.) will work for new services, too. Also, basic crud operations will be ready for additional entities due to generic repository pattern. To serve microservice architecture and continous delivery methodology, developers can create new assemblies according to business needs and put these assemblies between SmsProject.App.API and SmsProject.App.Operation. Or they can prefer to extend SmsProject.App.API.Core.

3. Deployment

I created an application pool and a site in IIS 7.5 in my local computer. Additionally, I created a publish profile for SmsProject.App.API. Deployments are two clicks away.
Better approach: We could prefer jenkins(or other tools) for automated and scheduled deployments. On the other hand, gitflow would be a good approach for code repo, and githubflow would be better. Githubflow is vital for continuous delivery.

4. Notes

Oauth would be nice for authentication and authorization. In this way, we could use this Sms provider as SaaS/SaaP.
A session object that is populated in another interceptor beside CallerInfo in Toolkit would be good for identifying clients.
Operation and API.Core assemblies could be splitted into more assemblies according to business needs, microservice support and continuous delivery support, if this project would be a real project.
Non-relational/distributed/clustered database like redis/hadoop/memcache would be better with high transactional operations like sms sending.


Important note: Please don’t forget adding company records to BS_Country before testing the code. You can use GET /countries.json or or xml for table creation.

Thank you for reading!
