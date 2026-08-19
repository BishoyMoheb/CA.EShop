# CA.EShop
It is a C# Clean Architecture solution that has its business logic independent of frameworks, databases, UI, and external services. 

The solution uses records (which is a C# type designed primarily for storing data where the values themselves are important) for Product commands (query get product, update, delete, and create product) along with their command handlers.

The solution uses also Mod_Product as module for adding routes for IEndpointRouteBuilder mapping functions 
which uses MediatR ISender interface methods for sending requests/commands and receiving a response.

The solution uses PostgreSQL as its open-source relational database management system (RDBMS).
