# Generic-CQRS
Generic CQRS in Clean Architecture 
#
Due to my need in make some generic classes in CQRS pattern for simpler usage,
I created some generic Command and Queries in Clean Architecture Template from @jasontaylordev. <br />
This is just a simple usage of Generic CQRS Implementations, you can add or modify it base on your usage in your applications or businesses. <br />
In some implementations, like <b> GenericDatatableQuery </b> I'v left a note about pagination implementation, you can implement some pagination based on your application, if you want of course.
<br> <br>
This is the folder structure in <b> Application.csproj </b>
<br>
![GenericCQRS Folder](https://user-images.githubusercontent.com/65845296/166886919-c17c475c-6221-447c-a572-e5f7080ce635.PNG)
<br>
<br>
After that you should Inject every Command or Queries that are using these generic Command or Query handlers becouse MediatR doesn't understand this level of generic implementation and it won't inject our GenericHandlers as a service in DI container.
The simplest way to inject our hanlders, has implemented in a file named <b> GenericCQRSDI.cs </b> in <b> Application.csproj </b>, and the main method named 
<b> AddGenericCQRSCommandQueries </b> has called in 
<b> Application.csproj > DependencyInjection.cs </b> file.
  <code> services.AddGenericCQRSCommandQueries(); </code>
  <br>
  <br>
  The only thing you should done is to set type of your entity and your DTO in any method in <b> GenericCQRSDI.cs </b> file,something like code below.
  This is just for Datatable injections to demonstrate how to use it,you can do this with other methods or you can create a method to inject Generic CRUD for your <b> Entity </b> and <b> DTO </b> in one method.
  <br>
  <code>
  
  
      private static void InjectDatatables(IServiceCollection services)
      {
          foreach (var item in GetDatatablesDictionary())
          {
  
              var pagedResponse = typeof(List<>).MakeGenericType(new Type[] { item.Value });
              var pagedQuery = typeof(GenericDatatableQuery<,>).MakeGenericType(new Type[] { item.Key, item.Value });
              services.AddScoped(typeof(IRequestHandler<,>).MakeGenericType(new Type[] { pagedQuery, pagedResponse })
                  , typeof(GenericDatatableQueryHandler<,>).MakeGenericType(new Type[] { item.Key, item.Value }));
  
          }
  
          static Dictionary<Type, Type> GetDatatablesDictionary()
          {
  
              return new Dictionary<Type, Type>
              {
                  // { typeof(your entity),typeof(your DTO) },
              };
  
          }
      }
  
  </code>
  
  With these implementation our handlers were injected automaticaly in starting application.
  #
  Please feel free to modify my implementations , this is just a starting step and has a lots of work to be an advance implementation of Generic CQRS pattern in Clean Architecture.
