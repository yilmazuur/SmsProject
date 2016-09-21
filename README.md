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
